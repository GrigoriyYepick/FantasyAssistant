using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyAssistant.DI;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInternalServices(this IServiceCollection services, IConfiguration configuration)
    {
        var installers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.ExportedTypes)
            .Where(type => typeof(IInstaller).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        foreach (var custom in installers)
        {
            custom.AddServices(services, configuration);
        }

        return services;
    }
}

internal sealed class DependenciesRegistry
{
    public static void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        var installers = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.ExportedTypes)
            .Where(type => typeof(IInstaller).IsAssignableFrom(type) && type is { IsInterface: false, IsAbstract: false })
            .Select(Activator.CreateInstance)
            .Cast<IInstaller>()
            .ToList();

        foreach (var custom in installers)
        {
            custom.AddServices(services, configuration);
        }
    }
}