namespace Qhse.Api.Services;

public static class ReferenceGenerator
{
    public static string New(string prefix)
    {
        return $"{prefix}-{DateTime.UtcNow:yyyyMMddHHmmss}-{Random.Shared.Next(100, 999)}";
    }
}
