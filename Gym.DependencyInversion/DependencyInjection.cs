using Gym.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Gym.Helpers.Utils;
using Gym.Helpers.Enums;
using Gym.Services.AutoMapperProfile;
using Gym.Data.Repositories;
using Gym.Domain.Interfaces;
using Gym.Data.UnitOfWork;
using Gym.Infrastructure.Smtp;

namespace Gym.DependencyInversion;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraInjection(this IServiceCollection services)
    {
        RegisterDbConstext(services);
        RegisterServices(services);
        RegisterBusiness(services);

        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        services.AddScoped(typeof(ISmtpSender), typeof(SmtpNetMailAdapter));
        services.AddAutoMapper(typeof(AutoMapperProfile));
        return services;
    }

    private static void RegisterDbConstext(IServiceCollection services)
    {
        switch (InfraHelpers.GetConnectionString().ProviderName)
        {
            case ProvidersTypes.SqlServer:
                services.AddDbContext<DataContext>(options => options.UseSqlServer(InfraHelpers.GetConnectionString().ConnectionString,
                                                   x => x.MigrationsAssembly(typeof(DataContext).Assembly.FullName)));
                break;

            default:
                throw new Exception("Provider not implemented");
        }
    }

    private static void AddDependencyInjectin(IServiceCollection services, string assemblyClass,
                                         string assemblyInterface)
    {
        foreach (var classType in AssemblyHelpers.GetClasses(assemblyClass))
        {
            var interfaces = AssemblyHelpers.GetInterfaces(assemblyInterface)
                                            .Where(x => x.IsAssignableFrom(classType));

            foreach (var interfaceType in interfaces)
                services.AddScoped(interfaceType, classType);
        }
    }

    private static void RegisterServices(IServiceCollection services)
    {
        string assemblyServiceLayer = "Gym.Services";
        AddDependencyInjectin(services, assemblyServiceLayer, assemblyServiceLayer);
    }

    private static void RegisterBusiness(IServiceCollection services)
    {
        string assemblyBusinessLayer = "Gym.Business";
        AddDependencyInjectin(services, assemblyBusinessLayer, assemblyBusinessLayer);
    }
}
