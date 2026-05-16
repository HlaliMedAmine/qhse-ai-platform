using Qhse.Api.Domain.Enums;

namespace Qhse.Api.Services;

public static class RiskScoring
{
    public static RiskLevel GetLevel(int score)
    {
        return score switch
        {
            >= 16 => RiskLevel.Critical,
            >= 10 => RiskLevel.High,
            >= 5 => RiskLevel.Moderate,
            _ => RiskLevel.Low
        };
    }
}
