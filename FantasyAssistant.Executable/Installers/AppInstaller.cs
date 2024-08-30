using FantasyAssistant.DI;
using FantasyAssistant.Executable.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyAssistant.Executable.Installers;

public sealed class AppInstaller : IInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IPlayersPromptBuilderFactory, PlayersPromptBuilderFactory>();
        services.AddSingleton<IPromptBuilder, PromptBuilder>();
    }
}