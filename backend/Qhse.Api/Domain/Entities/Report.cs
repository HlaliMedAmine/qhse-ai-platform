namespace Qhse.Api.Domain.Entities;

public sealed class Report : AuditableEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
    public string UploadedBy { get; set; } = string.Empty;
    public DateOnly UploadedAt { get; set; }
    public int SizeKb { get; set; }
    public string? BlobUri { get; set; }
}
