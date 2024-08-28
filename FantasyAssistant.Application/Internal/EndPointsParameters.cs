using Microsoft.Extensions.Configuration;

namespace FantasyAssistant.Application.Internal;

internal sealed class EndPointsParameters : IEndPointsParameters
{
    public EndPointsParameters(IConfiguration configuration)
    {
        Bootstrap = configuration["Endpoints:Bootstrap"]!;
        Fixtures = configuration["Endpoints:Fixtures"]!;
        Team = configuration["Endpoints:Team"]!;
    }

    public string Bootstrap { get; }

    public string Fixtures { get; }

    public string Team { get; }
}