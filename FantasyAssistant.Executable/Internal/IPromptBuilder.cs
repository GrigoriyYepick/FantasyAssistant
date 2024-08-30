using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Executable.Internal;

public interface IPromptBuilder
{
    string BuildPrompt(MyTeamData myTeamData, FantasyData bestPlayersData);
}