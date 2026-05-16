using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Services;

public static class EnumFormatter
{
    public static string ToApiValue(this Severity value) => value.ToString().ToLowerInvariant();

    public static string ToApiValue(this RecordStatus value) => value switch
    {
        RecordStatus.InProgress => "in_progress",
        _ => value.ToString().ToLowerInvariant()
    };

    public static string ToApiValue(this AuditStatus value) => value.ToString().ToLowerInvariant();

    public static string ToApiValue(this RiskLevel value) => value.ToString().ToLowerInvariant();

    public static string ToApiValue(this RiskCategory value) => value switch
    {
        RiskCategory.Quality => "Quality",
        RiskCategory.Hygiene => "Hygiene",
        RiskCategory.Safety => "Safety",
        RiskCategory.Environment => "Environment",
        _ => value.ToString()
    };
}
