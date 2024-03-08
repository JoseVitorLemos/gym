using System.Text;
using Gym.Domain.Enums;
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(CustomConfiguration.GetJWTSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false
            };
        });

        var allRoles = Enum.GetNames(typeof(Roles)).ToList();
        var allValidRoles = allRoles.Where(x => !x.Contains(Roles.EmailConfirmation.ToString())).ToList();

        var filterPersonal = new string[] { Roles.FitnessClient.ToString(), Roles.EmailConfirmation.ToString() };
        var personal = allRoles.Where(x => !filterPersonal.Contains(x));

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AllUsers", policy => policy.RequireRole(allRoles));
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("AllValidUsers", policy => policy.RequireRole(allValidRoles));
        });

        services.AddAuthorization(options =>
        {
            options.AddPolicy("Personal", policy => policy.RequireRole(personal));
        });

        return services;
    }
}
