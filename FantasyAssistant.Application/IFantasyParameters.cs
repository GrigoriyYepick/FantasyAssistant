namespace FantasyAssistant.Application;

public interface IFantasyParameters
{
    string TeamId { get; }

    string LastGameWeek { get; }

    IEndPointsParameters EndPoints { get; }
}