using Microsoft.Extensions.Configuration;

namespace FantasyAssistant.Application.Internal;

internal sealed class FantasyParameters : IFantasyParameters
{
    public FantasyParameters(IConfiguration configuration)
    {
        TeamId = configuration["Fantasy:TeamId"]!;
        LastGameWeek = configuration["Fantasy:LastGameWeek"]!;
        EndPoints = new EndPointsParameters(configuration);
    }

    public string TeamId { get; }

    public string LastGameWeek { get; }

    public IEndPointsParameters EndPoints { get; }
}