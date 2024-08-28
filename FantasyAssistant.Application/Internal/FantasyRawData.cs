using FantasyAssistant.Application.Model;

namespace FantasyAssistant.Application.Internal;

internal record FantasyRawData(
    IReadOnlyCollection<PlayerPick> MyPlayers,
    IReadOnlyCollection<Player> AllPlayers,
    IReadOnlyCollection<Fixture> Fixtures,
    IReadOnlyCollection<Team> Teams);