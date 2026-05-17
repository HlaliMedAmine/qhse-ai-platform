using System.ClientModel;
using System.Text.Json;
using OpenAI.Chat;
using Qhse.Api.Contracts.Ai;
using Qhse.Api.Domain.Entities;
using Qhse.Api.Domain.Enums;
using Qhse.Api.Infrastructure.Repositories;

namespace Qhse.Api.Services;

public sealed class AiAnalysisService(
    IRepository<AiAnalysisLog> repository,
    IAzureOpenAiChatClientFactory chatClientFactory,
    ILogger<AiAnalysisService> logger) : IAiAnalysisService
{
    private static readonly JsonSerializerOptions JsonOptions = new(JsonSerializerDefaults.Web);

    public async Task<AiAnalyzeResponse> AnalyzeAsync(AiAnalyzeRequest request, CancellationToken cancellationToken)
    {
        var prompt = BuildPrompt(request);
        logger.LogInformation(
            "Sending QHSE AI analysis request to Azure OpenAI deployment {DeploymentName}. Prompt length: {PromptLength}.",
            chatClientFactory.DeploymentName,
            prompt.Length);

        var response = await CallAzureOpenAiAsync(prompt, cancellationToken);

        await TrySaveAnalysisLogAsync(request, response, cancellationToken);
        return response;
    }

    private async Task<AiAnalyzeResponse> CallAzureOpenAiAsync(string prompt, CancellationToken cancellationToken)
    {
        var chatClient = chatClientFactory.CreateClient();

        ChatCompletion completion = await chatClient.CompleteChatAsync(
            [
                new SystemChatMessage("""
                    You are a senior QHSE expert assistant.
                    Analyze the user-provided QHSE text and return only valid JSON matching the requested schema.
                    Be concise, practical, and suitable for an enterprise QHSE dashboard.
                    """),
                new UserChatMessage(prompt)
            ],
            CreateChatOptions(),
            cancellationToken);

        var json = completion.Content.FirstOrDefault()?.Text;
        if (string.IsNullOrWhiteSpace(json))
        {
            throw new InvalidOperationException("Azure OpenAI returned an empty analysis response.");
        }

        try
        {
            var parsed = JsonSerializer.Deserialize<AzureOpenAiAnalysisResult>(json, JsonOptions)
                ?? throw new InvalidOperationException("Azure OpenAI returned a null analysis payload.");

            var riskLevel = NormalizeRiskLevel(parsed.RiskLevel);

            logger.LogInformation(
                "Azure OpenAI analysis completed with risk level {RiskLevel} using deployment {DeploymentName}.",
                riskLevel,
                chatClientFactory.DeploymentName);

            return new AiAnalyzeResponse(
                Summary: parsed.Summary,
                RiskLevel: riskLevel.ToApiValue(),
                CorrectiveActions: parsed.CorrectiveActions,
                Recommendations: parsed.Recommendations,
                Provider: $"AzureOpenAI/{chatClientFactory.DeploymentName}",
                GeneratedAtUtc: DateTimeOffset.UtcNow);
        }
        catch (JsonException ex)
        {
            logger.LogError(ex, "Azure OpenAI returned invalid JSON: {ResponseJson}", json);
            throw new InvalidOperationException("Azure OpenAI returned invalid JSON for the QHSE analysis response.", ex);
        }
    }

    private static ChatCompletionOptions CreateChatOptions()
    {
        return new ChatCompletionOptions
        {
            MaxOutputTokenCount = 1000,
            ResponseFormat = ChatResponseFormat.CreateJsonSchemaFormat(
                jsonSchemaFormatName: "qhse_ai_analysis",
                jsonSchema: BinaryData.FromString("""
                    {
                      "type": "object",
                      "properties": {
                        "summary": {
                          "type": "string",
                          "description": "A concise QHSE summary of the submitted text."
                        },
                        "riskLevel": {
                          "type": "string",
                          "enum": ["Low", "Moderate", "High", "Critical"],
                          "description": "Overall QHSE risk level."
                        },
                        "correctiveActions": {
                          "type": "array",
                          "items": { "type": "string" },
                          "description": "Immediate or near-term corrective actions."
                        },
                        "recommendations": {
                          "type": "array",
                          "items": { "type": "string" },
                          "description": "Preventive recommendations and management follow-up."
                        }
                      },
                      "required": ["summary", "riskLevel", "correctiveActions", "recommendations"],
                      "additionalProperties": false
                    }
                    """),
                jsonSchemaIsStrict: true)
        };
    }

    private async Task TrySaveAnalysisLogAsync(AiAnalyzeRequest request, AiAnalyzeResponse response, CancellationToken cancellationToken)
    {
        try
        {
            var log = new AiAnalysisLog
            {
                InputText = request.Text,
                Summary = response.Summary,
                RiskLevel = NormalizeRiskLevel(response.RiskLevel),
                CorrectiveActionsJson = JsonSerializer.Serialize(response.CorrectiveActions, JsonOptions),
                RecommendationsJson = JsonSerializer.Serialize(response.Recommendations, JsonOptions),
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
            Analyze this QHSE input and classify its operational risk.

            Source type: {request.SourceType ?? "free-text"}
            Source reference: {request.SourceReference ?? "n/a"}

            Return:
            - summary
            - riskLevel
            - correctiveActions
            - recommendations

            Text:
            {request.Text}
            """;
    }

    private static RiskLevel NormalizeRiskLevel(string value)
    {
        if (EnumParser.TryParse<RiskLevel>(value, out var riskLevel))
        {
            return riskLevel;
        }

        throw new InvalidOperationException($"Azure OpenAI returned an unsupported risk level: {value}");
    }

    private sealed record AzureOpenAiAnalysisResult(
        string Summary,
        string RiskLevel,
        IReadOnlyList<string> CorrectiveActions,
        IReadOnlyList<string> Recommendations);
}
