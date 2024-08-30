namespace FantasyAssistant.Executable.Internal;

public interface IPlayersPromptBuilder<in T>
{
    string Build();

    IPlayersPromptBuilder<T> WithGoalkeepers(IEnumerable<T> players);

    IPlayersPromptBuilder<T> WithDefenders(IEnumerable<T> players);

    IPlayersPromptBuilder<T> WithMidfielders(IEnumerable<T> players);

    IPlayersPromptBuilder<T> WithForwards(IEnumerable<T> players);
}