namespace FantasyAssistant.Executable.Internal;

internal sealed class PlayersPromptBuilderFactory : IPlayersPromptBuilderFactory
{
    public IPlayersPromptBuilder<T> Create<T>() => new PlayersPromptBuilder<T>();
}