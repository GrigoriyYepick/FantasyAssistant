namespace FantasyAssistant.Transport;

public sealed class PickDto
{
    public int element { get; set; }

    public bool is_captain { get; set; }

    public bool is_vice_captain { get; set; }
}