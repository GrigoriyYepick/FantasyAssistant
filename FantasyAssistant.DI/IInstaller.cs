using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FantasyAssistant.DI;

public interface IInstaller
{
    void AddServices(IServiceCollection services, IConfiguration configuration);
}