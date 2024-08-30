namespace FantasyAssistant.Application.Model;

public record MyTeamData(
    IReadOnlyCollection<PlayerPick> Goalkeepers,
    IReadOnlyCollection<PlayerPick> Defenders,
    IReadOnlyCollection<PlayerPick> Midfielders,
    IReadOnlyCollection<PlayerPick> Forwards);

public record FantasyData(
    IReadOnlyCollection<Player> Goalkeepers,
    IReadOnlyCollection<Player> Defenders,
    IReadOnlyCollection<Player> Midfielders,
    IReadOnlyCollection<Player> Forwards);