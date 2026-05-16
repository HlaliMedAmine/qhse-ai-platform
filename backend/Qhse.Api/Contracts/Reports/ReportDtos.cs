using System.ComponentModel.DataAnnotations;

namespace Qhse.Api.Contracts.Reports;

public sealed record ReportDto(
    Guid Id,
    string Reference,
    string Name,
    string Type,
    string UploadedBy,
    DateOnly UploadedAt,
    int SizeKb,
    string? BlobUri);

public sealed record CreateReportRequest(
    [Required, MaxLength(220)] string Name,
    [Required, MaxLength(20)] string Type,
    [Required, MaxLength(120)] string UploadedBy,
    DateOnly UploadedAt,
    [Range(1, int.MaxValue)] int SizeKb,
    string? BlobUri);

public sealed record UpdateReportRequest(
    [Required, MaxLength(220)] string Name,
    [Required, MaxLength(20)] string Type,
    [Required, MaxLength(120)] string UploadedBy,
    DateOnly UploadedAt,
    [Range(1, int.MaxValue)] int SizeKb,
    string? BlobUri);
