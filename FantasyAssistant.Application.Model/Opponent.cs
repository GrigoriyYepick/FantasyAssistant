namespace FantasyAssistant.Application.Model;

public sealed class Opponent
{
    public string TeamName { get; set; } = string.Empty;

    public int? Strength { get; set; }

    public override string ToString()
    {
        return FormattableString.Invariant($"{TeamName}, strength: {Strength}");
    }
}