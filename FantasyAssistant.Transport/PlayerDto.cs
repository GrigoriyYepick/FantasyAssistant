namespace FantasyAssistant.Transport;

public class PlayerDto
{
    public int id { get; set; }

    public string web_name { get; set; }

    public int element_type { get; set; }

    public int team { get; set; }

    public double now_cost { get; set; }

    public int total_points { get; set; }

    public int? chance_of_playing_next_round { get; set; }

    public double value_form { get; set; }
}