using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Domain.Entities;

public sealed class NonConformity : AuditableEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string SourceReference { get; set; } = string.Empty;
    public Severity Severity { get; set; }
    public string Owner { get; set; } = string.Empty;
    public DateOnly DueDate { get; set; }
    public RecordStatus Status { get; set; }
    public Guid? IncidentId { get; set; }
    public Incident? Incident { get; set; }
    public Guid? AuditId { get; set; }
    public Audit? Audit { get; set; }
}
