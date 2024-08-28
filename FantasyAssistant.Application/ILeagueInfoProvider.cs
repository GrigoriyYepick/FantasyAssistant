using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Application;

public interface ILeagueInfoProvider
{
    Task<FantasyData> ReadDataAsync();
}