using System.Configuration;
using Gym.Helpers.Enums;
using Gym.Helpers.Exceptions;
using Gym.Helpers.Validations;

namespace Gym.Helpers.Utils;

public class Providers
{
    public ProvidersTypes ProviderName { get; private set; }
    public string ConnectionString { get; private set; }

    public Providers()
    {
        Enum.TryParse(ConfigurationManager.AppSettings["provider"], true, out ProvidersTypes provider);
        GlobalException.When(!EnumValidations.IsValidEnum<ProvidersTypes>(provider), "ProvaiderName cannot be null");

        ConnectionStringSettings stringSettings = ConfigurationManager.ConnectionStrings[provider.ToString()];
        GlobalException.When(stringSettings.Name != provider.ToString(), "Connection name does not represent Provider Name");
        GlobalException.When(string.IsNullOrEmpty(stringSettings.ConnectionString), "ConnectionString cannot be null");

        ProviderName = provider;
        ConnectionString = stringSettings.ConnectionString;
    }
}
