namespace FantasyAssistant.Application.Model;

public record FantasyData(
    IReadOnlyCollection<PlayerPick> MyPlayers,
    IReadOnlyCollection<Player> Goalkeepers,
    IReadOnlyCollection<Player> Defenders,
    IReadOnlyCollection<Player> Midfielders,
    IReadOnlyCollection<Player> Forwards);