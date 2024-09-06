using FantasyAssistant.Application.Internal;
using FantasyAssistant.DI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyAssistant.Application.Installers;

public sealed class AppInstaller : IInstaller
{
    public void AddServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IFantasyApiService, FantasyApiService>();
        services.AddSingleton<IFantasyDataProvider, FantasyDataProvider>();
        services.AddSingleton<ILeagueInfoProvider, LeagueInfoProvider>();
    }
}