using Microsoft.Extensions.Configuration;

namespace FantasyAssistant.Executable.Internal;

internal static class ConfigurationExtensions
{
    public static string GetSetting(this IConfiguration configuration, string path)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(path, nameof(path));

        return configuration[path] ??
               throw new InvalidOperationException(FormattableString.Invariant($"{path} is not specified"));
    }

}