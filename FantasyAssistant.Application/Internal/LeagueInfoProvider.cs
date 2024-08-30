using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Application.Internal;

internal sealed class LeagueInfoProvider : ILeagueInfoProvider
{
    private readonly IFantasyDataProvider _fantasyDataProvider;

    public LeagueInfoProvider(IFantasyDataProvider fantasyDataProvider)
    {
        _fantasyDataProvider = fantasyDataProvider;
    }

    public async Task<(MyTeamData MyTeam, FantasyData AllPlayers)> ReadDataAsync()
    {
        var rawData = await _fantasyDataProvider.ReadRawDataAsync().ConfigureAwait(false);
        var teamsMap = rawData.Teams.ToDictionary(team => team.Id);

        MapMyPicks(rawData.MyPlayers, rawData.AllPlayers, teamsMap);

        var myGoalkeepers = FilterPlayersByType(rawData.MyPlayers, PlayerType.Goalkeeper);
        var myDefenders = FilterPlayersByType(rawData.MyPlayers, PlayerType.Defender);
        var myMidfielders = FilterPlayersByType(rawData.MyPlayers, PlayerType.Midfielder);
        var myForwards = FilterPlayersByType(rawData.MyPlayers, PlayerType.Forward);

        var myFantasyData = new MyTeamData(
            myGoalkeepers,
            myDefenders,
            myMidfielders,
            myForwards);

        var goalkeepers = FilterPlayersByType(rawData.AllPlayers, PlayerType.Goalkeeper, teamsMap, rawData.Fixtures);
        var defenders = FilterPlayersByType(rawData.AllPlayers, PlayerType.Defender, teamsMap, rawData.Fixtures);
        var midfielders = FilterPlayersByType(rawData.AllPlayers, PlayerType.Midfielder, teamsMap, rawData.Fixtures);
        var forwards = FilterPlayersByType(rawData.AllPlayers, PlayerType.Forward, teamsMap, rawData.Fixtures);

        var bestPlayersData = new FantasyData(
            goalkeepers,
            defenders,
            midfielders,
            forwards);

        return (myFantasyData, bestPlayersData);
    }

    private static void MapMyPicks(
        IReadOnlyCollection<PlayerPick> picks,
        IReadOnlyCollection<Player> allPlayers,
        IReadOnlyDictionary<int, Team> teams)
    {
        var allPlayersMap = allPlayers.ToDictionary(player => player.Id);

        foreach (var playerPick in picks)
        {
            if (allPlayersMap.TryGetValue(playerPick.Id, out var player))
            {
                player.Team = teams[player.TeamId];
                playerPick.Player = player;
            }
        }
    }

    private static List<PlayerPick> FilterPlayersByType(
        IReadOnlyCollection<PlayerPick> players,
        PlayerType playerType)
    {
        return players
            .Where(pick => pick.Player is not null && pick.Player.Type == playerType)
            .ToList();
    }

    private static List<Player> FilterPlayersByType(
        IReadOnlyCollection<Player> players,
        PlayerType playerType,
        IReadOnlyDictionary<int, Team> teams,
        IReadOnlyCollection<Fixture> fixtures)
    {
        const int count = 20;

        var filteredPlayers = players
            .Where(p => p.Type == playerType && p.ChanceOfPlaying is null or > 70 && p.Points > 0)
            .OrderByDescending(p => p.Form)
            .ThenByDescending(p => p.Points)
            .Take(count)
            .ToList();

        foreach (var player in filteredPlayers)
        {
            player.NextFixtures = GetNextOpponents(player, teams, fixtures);
            player.Team = teams[player.TeamId];
        }

        return filteredPlayers;
    }

    private static List<Opponent> GetNextOpponents(
        Player player,
        IReadOnlyDictionary<int, Team> teams,
        IReadOnlyCollection<Fixture> fixtures,
        int numOpponents = 3)
    {
        var teamId = player.TeamId;
        var upcomingOpponents = new List<Opponent>();

        foreach (var fixture in fixtures.Where(fixture => fixture is { EventId: not null, IsFinished: false }))
        {
            if (upcomingOpponents.Count < numOpponents)
            {
                var opponentTeamId = fixture.TeamAway == teamId ? fixture.TeamHome : fixture.TeamAway;

                upcomingOpponents.Add(new Opponent
                {
                    TeamName = teams[opponentTeamId].Name,
                    Strength = teams[opponentTeamId].Strength
                });
            }
        }

        return upcomingOpponents.Count > 0 ? upcomingOpponents :
        [
            new() { TeamName = "No Upcoming Matches", Strength = null }
        ];
    }
}