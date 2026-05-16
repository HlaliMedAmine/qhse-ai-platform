using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Domain.Entities;

public sealed class Audit : AuditableEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Scope { get; set; } = string.Empty;
    public string Auditor { get; set; } = string.Empty;
    public DateOnly AuditDate { get; set; }
    public int Score { get; set; }
    public AuditStatus Status { get; set; }

    public ICollection<NonConformity> NonConformities { get; set; } = [];
}
