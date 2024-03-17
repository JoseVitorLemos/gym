using Gym.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gym.Helpers.Utils;
using Gym.Helpers.Enums;
using Gym.Services.AutoMapperProfile;
using Gym.Data.Repositories;
using Gym.Data.UnitOfWork;
using Gym.Helpers.ConfigurationManager;
using Gym.Domain.Interfaces;

namespace Gym.DependencyInversion;

public static class DependencyInjection
{
    private static string[] AssemblysNames = new string[] { "Gym.Services", "Gym.Business", "Gym.Infrastructure", "Gym.Helpers" };

    public static IServiceCollection AddInfraInjection(this IServiceCollection services)
    {
        RegisterDbConstext(services);
        RegisterServicesFromAssembly(services, AssemblysNames);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddAutoMapper(typeof(AutoMapperProfile));
        return services;
    }

    private static void RegisterDbConstext(IServiceCollection services)
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
    }

    private static void AddDependencyInjectin(IServiceCollection services, string assemblyName)
    {
        foreach (var classType in AssemblyHelpers.GetClasses(assemblyName))
        {
            var interfacesType = AssemblyHelpers.GetInterfaces(assemblyName)
                                            .Where(x => x.IsAssignableFrom(classType));

            foreach (var interfaceType in interfacesType)
                services.AddScoped(interfaceType, classType);
        }
    }

    private static void RegisterServicesFromAssembly(IServiceCollection services, string[] assemblysNames)
    {
        foreach (var assemblyName in assemblysNames)
            AddDependencyInjectin(services, assemblyName);
    }
}
