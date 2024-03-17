using Gym.Helpers.Exceptions;

namespace Gym.Helpers.ConfigurationManager.Types;

public class CachingRedisSettingsType
{
    public string InstanceName { get; private set; }
    public string Host { get; private set; }

    public CachingRedisSettingsType(string instanceName, string host)
    {
        ValidationsSettings(instanceName, host);

        InstanceName = instanceName;
        Host = host;
    }

    private void ValidationsSettings(string instanceName, string host)
    {
        GlobalException.When(string.IsNullOrEmpty(instanceName), "Invalid Redis email provided");
        GlobalException.When(string.IsNullOrEmpty(host), "Invalid Redis host provided");
    }
}
