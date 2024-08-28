namespace FantasyAssistant.Application.Model;

public sealed class Fixture
{
    public int? EventId { get; set; }

    public int TeamAway { get; set; }

    public int TeamHome { get; set; }

    public bool IsFinished { get; set; }
}