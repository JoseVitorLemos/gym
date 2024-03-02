using System.Configuration;
using Clean.Arch.Helpers.Exceptions;

namespace Clean.Arch.Helpers.Utils;

public static class InfraHelpers
{
    public static Providers GetConnectionString()
        => new Providers();

    public static string GetAuthSecret()
    {
        string? secret = ConfigurationManager.AppSettings["secret"];
        GlobalException.When(string.IsNullOrEmpty(secret), "Auth secret cannot be null");
        return secret ?? string.Empty;
    }

    public static int GetExpireTime()
    {
        string? setting = ConfigurationManager.AppSettings["expireTime"];
        GlobalException.When(!int.TryParse(setting, out int expireTime), "Auth expire time cannot be null");
        return expireTime;
    }
}
