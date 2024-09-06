using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyAssistant.DI;

public static class AddSettingsExtensions
{
    public static void AddSettings<T>(this IServiceCollection services, IConfiguration configuration, string path)
        where T: class
    {
        ArgumentNullException.ThrowIfNull(configuration, nameof(configuration));
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        services.AddOptions<T>()
            .Bind(configuration.GetSection(path))
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}