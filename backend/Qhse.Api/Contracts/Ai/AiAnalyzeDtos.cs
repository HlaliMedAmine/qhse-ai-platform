using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.Ai;

public sealed record AiAnalyzeRequest(
    [Required, MinLength(5), MaxLength(8000)] string Text,
    string? SourceType,
    string? SourceReference);

public sealed record AiAnalyzeResponse(
    string Summary,
    string RiskLevel,
    IReadOnlyList<string> CorrectiveActions,
    IReadOnlyList<string> Recommendations,
    string Provider,
    DateTimeOffset GeneratedAtUtc);
