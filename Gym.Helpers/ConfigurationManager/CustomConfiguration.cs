using Microsoft.Extensions.Configuration;
using Gym.Helpers.ConfigurationManager.Types;

namespace Gym.Helpers.ConfigurationManager;

public static class CustomConfiguration
{
    private readonly static IConfiguration _configuration;

    static CustomConfiguration()
        => _configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();

    public static AppSettingsType GetAppSettings
        => _configuration.GetSection("AppSettings").Get<AppSettingsType>();

    public static ConnectionStringsType GetConnectionStrings
        => _configuration.GetSection("ConnectionStrings").Get<ConnectionStringsType>();

    public static SmtpSettingsType GetSmtpSettings
        => _configuration.GetSection("Smtp").Get<SmtpSettingsType>();

    public static JWTSettingsType GetJWTSettings
        => _configuration.GetSection("JwtSettings").Get<JWTSettingsType>();

}
