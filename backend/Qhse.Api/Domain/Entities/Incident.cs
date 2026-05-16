using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Domain.Entities;

public sealed class Incident : AuditableEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Site { get; set; } = string.Empty;
    public Severity Severity { get; set; }
    public RecordStatus Status { get; set; }
    public string ReportedBy { get; set; } = string.Empty;
    public DateOnly IncidentDate { get; set; }
    public string? Description { get; set; }

    public ICollection<NonConformity> NonConformities { get; set; } = [];
}
