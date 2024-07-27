using Microsoft.Extensions.DependencyInjection;
using Gym.Helpers.Utils;

namespace Gym.DependencyInversion.DependencyInjection;

public static class DependencyInjection
{
    private static readonly string[] AssemblysNames = { "Gym.Services", "Gym.Business", "Gym.Infrastructure", "Gym.Helpers" };

    public static IServiceCollection AddDependecyInjection(this IServiceCollection services)
    {
        AddInjectionFromAssembly(services, AssemblysNames);
        return services;
    }

    private static void AddInjectionFromAssembly(IServiceCollection services, string[] assemblysNames)
    {
        foreach (var assemblyName in assemblysNames)
            AddDependencyInjectin(services, assemblyName);
    }

    private static void AddDependencyInjectin(IServiceCollection services, string assemblyName)
    {
        foreach (var classType in AssemblyHelpers.GetClasses(assemblyName))
        {
            var interfacesType = AssemblyHelpers.GetInterfaces(assemblyName)
                                            .Where(x => x.IsAssignableFrom(classType));

            if (interfacesType.Any())
                services.AddScoped(interfacesType.First(), classType);
        }
    }
}