using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;

namespace Gym.Helpers.ConfigurationManager.Types;

public class AppSettingsType
{
    public string FrontEndUri { get; private set; }
    public ProvidersTypes Provider { get; private set; }

    public AppSettingsType(string frontEndUri, ProvidersTypes provider)
    {
        Validations(provider);

        FrontEndUri = frontEndUri;
        Provider = provider;
    }

    private void Validations(ProvidersTypes provider)
        => GlobalException.When(!EnumValidations.IsValidEnum<ProvidersTypes>(provider),
               "ProvaiderName cannot be null");
}
