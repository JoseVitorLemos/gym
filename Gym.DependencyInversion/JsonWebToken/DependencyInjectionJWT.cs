using System.Text;
using Gym.Helpers.ConfigurationManager;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Gym.DependencyInversion.Swagger;

public static class DependencyInjectionJWT
{
    public static IServiceCollection AddInfrastructureJWT(this IServiceCollection services)
    {
        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.RequireHttpsMetadata = false;
            x.SaveToken = true;
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfiguration.GetAppSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });
        return services;
    }
}
