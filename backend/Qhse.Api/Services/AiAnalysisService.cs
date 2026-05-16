using System.Text.Json;
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
            : throw new NotSupportedException("Azure OpenAI live integration is not enabled yet. Set UseMock=true or implement the provider client.");

        await TrySaveAnalysisLogAsync(request, response, cancellationToken);
        return response;
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
            logger.LogWarning(ex, "AI analysis was generated but could not be persisted. Returning the mock analysis response anyway.");
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
            Summary: $"Mock analysis: the submitted QHSE text indicates a {level} risk profile and requires documented follow-up.",
            RiskLevel: level.ToApiValue(),
            CorrectiveActions:
            [
                "Secure the affected area and confirm immediate controls are in place.",
                "Assign an accountable owner and target closure date.",
                "Record root-cause analysis and evidence of corrective action completion."
            ],
            Recommendations:
            [
                "Review the relevant procedure and training records.",
                "Trend similar events across sites for recurring causes.",
                "Escalate to QHSE leadership if residual risk remains High or Critical."
            ],
            Provider: "MockAzureOpenAI",
            GeneratedAtUtc: DateTimeOffset.UtcNow);
    }
}
