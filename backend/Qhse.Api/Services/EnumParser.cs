namespace Qhse.Api.Services;

public static class EnumParser
{
    public static bool TryParse<TEnum>(string value, out TEnum parsed)
        where TEnum : struct, Enum
    {
        var normalized = value.Replace("_", string.Empty).Replace("-", string.Empty);
        foreach (var name in Enum.GetNames<TEnum>())
        {
            if (string.Equals(name, normalized, StringComparison.OrdinalIgnoreCase))
            {
                parsed = Enum.Parse<TEnum>(name);
                return true;
            }
        }

        parsed = default;
        return false;
    }
}
