using System.Reflection;

namespace Gym.Helpers.Utils;

public static class AssemblyHelpers
{
    public static IEnumerable<Type> GetClasses(string assemblyName)
        => Assembly.Load(assemblyName).GetTypes()
                   .Where(c => c.IsClass && !c.IsAbstract && !c.IsGenericType && c.IsPublic);

    public static IEnumerable<Type> GetInterfaces(string assemblyName)
        => Assembly.Load(assemblyName)
                   .GetTypes().Where(i => i.IsInterface);
}
