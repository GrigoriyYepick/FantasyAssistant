using System.Text;
using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Executable.Internal;

internal sealed class PromptBuilder : IPromptBuilder
{
    private readonly IPlayersPromptBuilderFactory _playersPromptBuilderFactory;

    public PromptBuilder(
        IPlayersPromptBuilderFactory playersPromptBuilderFactory)
    {
        _playersPromptBuilderFactory = playersPromptBuilderFactory;
    }

    public string BuildPrompt(MyTeamData myTeamData, FantasyData bestPlayersData)
    {
        ArgumentNullException.ThrowIfNull(myTeamData, nameof(myTeamData));
        ArgumentNullException.ThrowIfNull(bestPlayersData, nameof(bestPlayersData));

        var sb = new StringBuilder();
        sb.Append("Hi! Based on the information provided below about the Fantasy English Premier League, please suggest specific transfers for my squad. ");
        sb.Append("The goal is to build a team with 2 goalkeepers, 5 defenders, 5 midfielders, and 3 forwards, staying within the total price limit of 100. ");
        sb.Append("Additionally, no more than 3 players should be from the same football team.");
        sb.AppendLine("Important: Please review my current squad and suggest transfers by replacing under-performing or less optimal players with better options. ");
        sb.Append("Ensure that each suggested transfer is position-specific (e.g., a goalkeeper for a goalkeeper, a midfielder for a midfielder).");
        sb.AppendLine();

        WithBestPlayers(bestPlayersData, sb);
        WithMyPlayers(myTeamData, sb);

        sb.AppendLine("Please suggest specific transfer recommendations by comparing my current players with the best available options. ");
        sb.Append("For each suggestion, mention the player I should transfer out and the player I should bring in. Please, add price and team info for both of them.");

        return sb.ToString();
    }

    private void WithBestPlayers(FantasyData bestPlayersData, StringBuilder sb)
    {
        sb.AppendLine("Here are the best players by position in the league right now:");

        var builder = _playersPromptBuilderFactory.Create<Player>();
        var playersPrompt = builder
            .WithGoalkeepers(bestPlayersData.Goalkeepers)
            .WithDefenders(bestPlayersData.Defenders)
            .WithMidfielders(bestPlayersData.Midfielders)
            .WithForwards(bestPlayersData.Forwards)
            .Build();

        sb.AppendLine(playersPrompt);
    }

    private void WithMyPlayers(MyTeamData teamData, StringBuilder sb)
    {
        sb.AppendLine("Here is my current team:");

        var builder = _playersPromptBuilderFactory.Create<PlayerPick>();
        var playersPrompt = builder
            .WithGoalkeepers(teamData.Goalkeepers)
            .WithDefenders(teamData.Defenders)
            .WithMidfielders(teamData.Midfielders)
            .WithForwards(teamData.Forwards)
            .Build();

        sb.AppendLine(playersPrompt);
    }
}