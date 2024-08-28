namespace FantasyAssistant.Transport;

public sealed class FixtureDto
{
    public int? id { get; set; }

    public int team_a { get; set; }

    public int team_h { get; set; }

    public bool finished { get; set; }
}