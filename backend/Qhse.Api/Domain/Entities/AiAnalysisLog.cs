using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Domain.Entities;

public sealed class AiAnalysisLog : AuditableEntity
{
    public string InputText { get; set; } = string.Empty;
    public string Summary { get; set; } = string.Empty;
    public RiskLevel RiskLevel { get; set; }
    public string CorrectiveActionsJson { get; set; } = "[]";
    public string RecommendationsJson { get; set; } = "[]";
    public string Provider { get; set; } = "AzureOpenAI";
}
