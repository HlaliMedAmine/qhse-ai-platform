using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.Audits;

public sealed record AuditDto(
    Guid Id,
    string Reference,
    string Scope,
    string Auditor,
    DateOnly Date,
    int Score,
    string Status);

public sealed record CreateAuditRequest(
    [Required, MaxLength(160)] string Reference,
    [Required, MaxLength(220)] string Scope,
    [Required, MaxLength(120)] string Auditor,
    DateOnly Date,
    [Range(0, 100)] int Score,
    [Required] string Status);

public sealed record UpdateAuditRequest(
    [Required, MaxLength(160)] string Reference,
    [Required, MaxLength(220)] string Scope,
    [Required, MaxLength(120)] string Auditor,
    DateOnly Date,
    [Range(0, 100)] int Score,
    [Required] string Status);
