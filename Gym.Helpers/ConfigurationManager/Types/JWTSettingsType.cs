using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;

namespace Gym.Helpers.ConfigurationManager.Types;

public class JWTSettingsType
{
    public int ExpireHours { get; private set; }
    public string Secret { get; private set; }

    public JWTSettingsType(int expireHours, string secret)
    {
        Validations(expireHours, secret);

        ExpireHours = expireHours;
        Secret = secret;
    }

    private void Validations(int expireHours, string secret)
    {
        GlobalException.When(expireHours < 1, "Auth expireHours cannot be 0");
        GlobalException.When(secret.IsNullOrWhiteSpace(), "Secret cannot be null");
    }
}
