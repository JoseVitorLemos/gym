using Clean.Arch.Data.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Clean.Arch.Helpers.Utils;
using Clean.Arch.Helpers.Enums;
using Clean.Arch.Services.AutoMapperProfile;
using Clean.Arch.Data.Repositories;
using Clean.Arch.Domain.Interfaces;
using Clean.Arch.Data.UnitOfWork;
using Clean.Arch.Infrastructure.Smtp;

namespace Clean.Arch.DependencyInversion;

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
        string assemblyServiceLayer = "Clean.Arch.Services";
        AddDependencyInjectin(services, assemblyServiceLayer, assemblyServiceLayer);
    }

    private static void RegisterBusiness(IServiceCollection services)
    {
        string assemblyBusinessLayer = "Clean.Arch.Business";
        AddDependencyInjectin(services, assemblyBusinessLayer, assemblyBusinessLayer);
    }
}
