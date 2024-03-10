namespace Gym.Helpers.Utils;

public static class GuidValidations
{
    public static bool IsValidGuid(this Guid value)
        => value != Guid.Empty &&
            Guid.TryParse(value.ToString(), out Guid guidResult);
}
