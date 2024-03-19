using Gym.Helpers.ConfigurationManager;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.DependencyInversion.Caching;

public static class CacheRedisInjection
{
    public static IServiceCollection AddInfrastructureRedis(this IServiceCollection services)
    {
        services.AddStackExchangeRedisCache(x => 
                {
                    x.Configuration = CustomConfiguration.RedisSettings.Host;
                });

        return services;
    }
}
