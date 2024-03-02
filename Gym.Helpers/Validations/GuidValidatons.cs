namespace Gym.Helpers.Utils;

public static class GuidValidations
{
    public static bool IsValid(Guid? value)
        => value.HasValue && value != Guid.Empty;
}
