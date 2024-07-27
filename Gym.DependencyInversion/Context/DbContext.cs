using Gym.Data.DatabaseContext;
using Gym.Helpers.ConfigurationManager;
using Gym.Helpers.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Gym.DependencyInversion.Context
{
    public static class DbContext
    {
        public static IServiceCollection AddDataContext(this IServiceCollection services)
        {
            switch (CustomConfiguration.AppSettings.Provider)
            {
                case ProvidersTypes.SqlServer:
                    services.AddDbContext<DataContext>(options =>
                            options.UseSqlServer(CustomConfiguration.ConnectionStrings.DefaultConnectionStrings,
                                x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
                break;

                default:
                throw new Exception("Provider not implemented");
            }

            return services;
        }
    }
}
