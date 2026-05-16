using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.NonConformities;

public sealed record NonConformityDto(
    Guid Id,
    string Reference,
    string Title,
    string Source,
    string Severity,
    string Owner,
    DateOnly DueDate,
    string Status,
    Guid? IncidentId,
    Guid? AuditId);

public sealed record CreateNonConformityRequest(
    [Required, MaxLength(180)] string Title,
    [Required, MaxLength(60)] string Source,
    [Required] string Severity,
    [Required, MaxLength(120)] string Owner,
    DateOnly DueDate,
    Guid? IncidentId,
    Guid? AuditId);

public sealed record UpdateNonConformityRequest(
    [Required, MaxLength(180)] string Title,
    [Required, MaxLength(60)] string Source,
    [Required] string Severity,
    [Required, MaxLength(120)] string Owner,
    DateOnly DueDate,
    [Required] string Status,
    Guid? IncidentId,
    Guid? AuditId);
