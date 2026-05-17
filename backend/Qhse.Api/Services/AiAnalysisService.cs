using System.Text.Json;
using Azure;
using Azure.AI.OpenAI;
using OpenAI.Chat;
using Microsoft.Extensions.Options;
using Qhse.Api.Contracts.Ai;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;
using Qhse.Api.Options;

namespace Qhse.Api.Services;

public sealed class AiAnalysisService(
    IRepository<AiAnalysisLog> repository,
    IOptions<AzureOpenAiOptions> options,
    ILogger<AiAnalysisService> logger) : IAiAnalysisService
{
    public async Task<AiAnalyzeResponse> AnalyzeAsync(AiAnalyzeRequest request, CancellationToken cancellationToken)
    {
        var prompt = BuildPrompt(request);
        logger.LogInformation("Preparing QHSE AI analysis prompt with {PromptLength} characters.", prompt.Length);

        var response = options.Value.UseMock
            ? CreateMockAnalysis(request.Text)
            : await CallAzureOpenAiAsync(prompt, cancellationToken);

        await TrySaveAnalysisLogAsync(request, response, cancellationToken);
        return response;
    }

    private async Task<AiAnalyzeResponse> CallAzureOpenAiAsync(string prompt, CancellationToken cancellationToken)
    {
        var opts = options.Value;
        var client = new AzureOpenAIClient(new Uri(opts.Endpoint), new AzureKeyCredential(opts.ApiKey));
        var chatClient = client.GetChatClient(opts.DeploymentName);

        var completion = await chatClient.CompleteChatAsync(
            [
                new SystemChatMessage("""
                    You are a QHSE expert assistant. Always respond with valid JSON only.
                    Format: {"summary":"...","riskLevel":"Low|Moderate|High|Critical","correctiveActions":["..."],"recommendations":["..."]}
                    """),
                new UserChatMessage(prompt)
            ],
            new ChatCompletionOptions { MaxOutputTokenCount = 1000 },
            cancellationToken);

        var json = completion.Value.Content[0].Text;

        try
        {
            var parsed = JsonSerializer.Deserialize<AzureOpenAiResult>(json, new JsonSerializerOptions { PropertyNameCaseInsensitive = true })
                ?? throw new InvalidOperationException("Null response from Azure OpenAI.");

            return new AiAnalyzeResponse(
                Summary: parsed.Summary,
                RiskLevel: parsed.RiskLevel,
                CorrectiveActions: parsed.CorrectiveActions,
                Recommendations: parsed.Recommendations,
                Provider: "AzureOpenAI/gpt-4o-mini",
                GeneratedAtUtc: DateTimeOffset.UtcNow);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to parse Azure OpenAI response: {Json}", json);
            return CreateMockAnalysis(prompt);
        }
    }

    private async Task TrySaveAnalysisLogAsync(AiAnalyzeRequest request, AiAnalyzeResponse response, CancellationToken cancellationToken)
    {
        try
        {
            var log = new AiAnalysisLog
            {
                InputText = request.Text,
                Summary = response.Summary,
                RiskLevel = Enum.Parse<RiskLevel>(response.RiskLevel, ignoreCase: true),
                CorrectiveActionsJson = JsonSerializer.Serialize(response.CorrectiveActions),
                RecommendationsJson = JsonSerializer.Serialize(response.Recommendations),
                Provider = response.Provider
            };

            await repository.AddAsync(log, cancellationToken);
            await repository.SaveChangesAsync(cancellationToken);
        }
        catch (Exception ex)
        {
            logger.LogWarning(ex, "AI analysis was generated but could not be persisted.");
        }
    }

    private static string BuildPrompt(AiAnalyzeRequest request)
    {
        return $"""
            You are a QHSE expert assistant. Analyze the following operational text.
            Return JSON with: summary, riskLevel, correctiveActions, recommendations.
            Source type: {request.SourceType ?? "free-text"}
            Source reference: {request.SourceReference ?? "n/a"}

            Text:
            {request.Text}
            """;
    }

    private static AiAnalyzeResponse CreateMockAnalysis(string text)
    {
        var normalized = text.ToLowerInvariant();
        var level = normalized.Contains("critical") || normalized.Contains("critique") || normalized.Contains("chemical") || normalized.Contains("chimique")
            ? RiskLevel.Critical
            : normalized.Contains("spill") || normalized.Contains("leak") || normalized.Contains("fuite") || normalized.Contains("non-conform")
                ? RiskLevel.High
                : RiskLevel.Moderate;

        return new AiAnalyzeResponse(
            Summary: $"Mock analysis: the submitted QHSE text indicates a {level} risk profile.",
            RiskLevel: level.ToApiValue(),
            CorrectiveActions: ["Secure the affected area.", "Assign an accountable owner.", "Record root-cause analysis."],
            Recommendations: ["Review the relevant procedure.", "Trend similar events.", "Escalate if risk remains High or Critical."],
            Provider: "MockAzureOpenAI",
            GeneratedAtUtc: DateTimeOffset.UtcNow);
    }

    private sealed record AzureOpenAiResult(
        string Summary,
        string RiskLevel,
        List<string> CorrectiveActions,
        List<string> Recommendations);
}