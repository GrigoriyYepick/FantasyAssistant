using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Application;

public interface ILeagueInfoProvider
{
    Task<(MyTeamData MyTeam, FantasyData AllPlayers)> ReadDataAsync();
}