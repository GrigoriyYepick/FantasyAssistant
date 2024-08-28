namespace FantasyAssistant.Application.Model;

public sealed class Player
{
    public int Id { get; set; }

    public string FullName { get; set; } = string.Empty;

    public PlayerType Type { get; set; }

    public int TeamId { get; set; }

    public Team Team { get; set; }

    public double Cost { get; set; }

    public int Points { get; set; }

    public double Form { get; set; }

    public int? ChanceOfPlaying { get; set; }

    public IReadOnlyCollection<Opponent> NextFixtures { get; set; } = new List<Opponent>();

    public override string ToString()
    {
        return FormattableString.Invariant(
            $"Name: {FullName}; Team: {Team.Name}; TeamStrength: {Team.Strength}; Chance of playing next game {GetChanceOfPlaying()}; Cost: {Cost}; Points: {Points}; Current form: {Form}; Next opponents: {GetOpponents()}");
    }

    private int GetChanceOfPlaying() => ChanceOfPlaying ?? 100;

    private string GetOpponents() =>
        NextFixtures.Count == 0
            ? "No opponents"
            : string.Join(',', NextFixtures.Select(opponent => opponent.ToString()));
}