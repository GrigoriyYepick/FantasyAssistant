using System.Text;

namespace FantasyAssistant.Executable.Internal;

internal sealed class PlayersPromptBuilder<T> : IPlayersPromptBuilder<T>
{
    private readonly StringBuilder _stringBuilder = new();

    public string Build() => _stringBuilder.ToString();

    public IPlayersPromptBuilder<T> WithGoalkeepers(IEnumerable<T> players) => WithPlayers("Goalkeepers", players);

    public IPlayersPromptBuilder<T> WithDefenders(IEnumerable<T> players) => WithPlayers("Defenders", players);

    public IPlayersPromptBuilder<T> WithMidfielders(IEnumerable<T> players) => WithPlayers("Midfielders", players);

    public IPlayersPromptBuilder<T> WithForwards(IEnumerable<T> players) => WithPlayers("Forwards", players);

    private IPlayersPromptBuilder<T> WithPlayers(string position, IEnumerable<T> players)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(position, nameof(position));
        ArgumentNullException.ThrowIfNull(players, nameof(players));

        var playersInfo = string.Join("\n", players.Select(player => player?.ToString()));

        _stringBuilder.AppendLine($"{position}:");
        _stringBuilder.AppendLine($"{playersInfo}");

        return this;
    }
}