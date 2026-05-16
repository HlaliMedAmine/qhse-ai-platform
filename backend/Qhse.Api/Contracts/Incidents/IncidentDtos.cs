using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.Incidents;

public sealed record IncidentDto(
    Guid Id,
    string Reference,
    string Title,
    string Site,
    string Severity,
    string Status,
    string ReportedBy,
    DateOnly Date,
    string? Description);

public sealed record CreateIncidentRequest(
    [Required, MaxLength(180)] string Title,
    [Required, MaxLength(120)] string Site,
    [Required] string Severity,
    [Required, MaxLength(120)] string ReportedBy,
    DateOnly Date,
    string? Description);

public sealed record UpdateIncidentRequest(
    [Required, MaxLength(180)] string Title,
    [Required, MaxLength(120)] string Site,
    [Required] string Severity,
    [Required] string Status,
    [Required, MaxLength(120)] string ReportedBy,
    DateOnly Date,
    string? Description);
