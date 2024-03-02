using System.ComponentModel;

namespace Clean.Arch.Helpers.Utils;

public static class EnumHelpers
{
    public static string GetDescription(this Enum value)
    {
        if (value is null)
            return string.Empty;

        var attribute = value.GetType()
            .GetField(value.ToString())
            ?.GetCustomAttributes(typeof(DescriptionAttribute), false)
            .SingleOrDefault() as DescriptionAttribute;

        return attribute?.Description ?? string.Empty;
    }
}
