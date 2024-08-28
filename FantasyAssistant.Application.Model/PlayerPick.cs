namespace FantasyAssistant.Application.Model;

public sealed class PlayerPick
{
    public Player? Player { get; set; } = default;

    public int Id { get; set; }

    public bool IsCaptain { get; set; }

    public bool IsViceCaptain { get; set; }

    public override string ToString()
    {
        return FormattableString.Invariant($"{Player.ToString()}; Player is captain: {IsCaptain}; Player is vice captain: {IsViceCaptain}");
    }
}