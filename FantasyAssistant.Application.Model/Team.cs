namespace FantasyAssistant.Application.Model;

public sealed class Team
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public int Points { get; set; }

    public int Strength { get; set; }
}