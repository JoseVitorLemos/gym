namespace Gym.Helpers.Utils;

public static class GuidValidations
{
    public static bool IsValid(this Guid value)
        => value != Guid.Empty;
}