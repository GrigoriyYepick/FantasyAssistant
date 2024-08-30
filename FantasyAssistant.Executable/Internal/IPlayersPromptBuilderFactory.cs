namespace FantasyAssistant.Executable.Internal;

internal interface IPlayersPromptBuilderFactory
{
    IPlayersPromptBuilder<T> Create<T>();
}