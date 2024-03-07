using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;

namespace Gym.Helpers.ConfigurationManager.Types;

public class AppSettingsType
{
    public string FrontEndUri { get; private set; }
    public ProvidersTypes Provider { get; private set; }
    public int ExpireHours { get; private set; }
    public string Secret { get; private set; }

    public AppSettingsType(string frontEndUri, ProvidersTypes provider,
            int expireHours, string secret)
    {
        Validations(provider, expireHours, secret);

        FrontEndUri = frontEndUri;
        Provider = provider;
        ExpireHours = expireHours;
        Secret = secret;
    }

    private void Validations(ProvidersTypes provider, int expireHours, string secret)
    {
        GlobalException.When(!EnumValidations.IsValidEnum<ProvidersTypes>(provider), "ProvaiderName cannot be null");
        GlobalException.When(expireHours < 1, "Auth expireHours cannot be 0");
        GlobalException.When(secret.IsNullOrWhiteSpace(), "Secret cannot be null");
    }
}
