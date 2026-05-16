using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Domain.Entities;

public sealed class Risk : AuditableEntity
{
    public string Reference { get; set; } = string.Empty;
    public string Hazard { get; set; } = string.Empty;
    public RiskCategory Category { get; set; }
    public int Likelihood { get; set; }
    public int Impact { get; set; }
    public string Mitigation { get; set; } = string.Empty;

    public int Score => Likelihood * Impact;
}
