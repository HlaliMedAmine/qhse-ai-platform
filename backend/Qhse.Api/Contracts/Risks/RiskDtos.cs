using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.Risks;

public sealed record RiskDto(
    Guid Id,
    string Reference,
    string Hazard,
    string Category,
    int Likelihood,
    int Impact,
    int Score,
    string Level,
    string Mitigation);

public sealed record CreateRiskRequest(
    [Required, MaxLength(220)] string Hazard,
    [Required] string Category,
    [Range(1, 5)] int Likelihood,
    [Range(1, 5)] int Impact,
    [Required, MaxLength(500)] string Mitigation);

public sealed record UpdateRiskRequest(
    [Required, MaxLength(220)] string Hazard,
    [Required] string Category,
    [Range(1, 5)] int Likelihood,
    [Range(1, 5)] int Impact,
    [Required, MaxLength(500)] string Mitigation);
