namespace Gym.Helpers.Validations;

public static class EnumValidations
{
    public static bool IsValidEnum<T>(object value) where T : struct
    {
        var enumString = Convert.ToString(value);

        if (String.IsNullOrEmpty(enumString) || !typeof(T).IsEnum)
            return false;

        return Enum.TryParse(enumString, true, out T parsedEnumValue) && Enum.IsDefined(typeof(T), parsedEnumValue);
    }
}
