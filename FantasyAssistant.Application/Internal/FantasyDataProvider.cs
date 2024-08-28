using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FantasyAssistant.Application.Internal;

internal sealed class FantasyDataProvider : IFantasyDataProvider
{
    private readonly IMapper _mapper;
    private readonly IFantasyParameters _parameters;

    public FantasyDataProvider(
        IMapper mapper,
        IFantasyParameters parameters)
    {
        _mapper = mapper;
        _parameters = parameters;
    }

    public async Task<FantasyRawData> ReadRawDataAsync()
    {
        using var client = new HttpClient();
        var bootstrapStats = await client.GetStringAsync(_parameters.EndPoints.Bootstrap).ConfigureAwait(false);

        var fixtures = await ReadFixtures(client).ConfigureAwait(false);
        var allPlayers = ReadAllPlayers(bootstrapStats);
        var teams = ReadTeams(bootstrapStats);
        var playersPicks = await ReadMyPlayers(client).ConfigureAwait(false);

        return new FantasyRawData(
            playersPicks,
            allPlayers,
            fixtures,
            teams);
    }

    private IReadOnlyCollection<Player> ReadAllPlayers(string jsonResponse)
    {
        var playersDto = FetchCollection<List<PlayerDto>>(jsonResponse, "elements");
        return _mapper.Map<IReadOnlyCollection<Player>>(playersDto);
    }

    private async Task<IReadOnlyCollection<PlayerPick>> ReadMyPlayers(HttpClient client)
    {
        var myTeamUri = string.Format(_parameters.EndPoints.Team, _parameters.TeamId, _parameters.LastGameWeek);
        var response = await client.GetStringAsync(myTeamUri).ConfigureAwait(false);
        var playersPicksDto = FetchCollection<List<PickDto>>(response, "picks");
        return _mapper.Map<IReadOnlyCollection<PlayerPick>>(playersPicksDto);
    }

    private async Task<IReadOnlyCollection<Fixture>> ReadFixtures(HttpClient client)
    {
        var response = await client.GetStringAsync(_parameters.EndPoints.Fixtures).ConfigureAwait(false);
        var fixturesDto = FetchData<List<FixtureDto>>(response);
        return _mapper.Map<IReadOnlyCollection<Fixture>>(fixturesDto);
    }

    private IReadOnlyCollection<Team> ReadTeams(string jsonResponse)
    {
        var teamsDto = FetchCollection<List<TeamDto>>(jsonResponse, "teams");
        return _mapper.Map<IReadOnlyCollection<Team>>(teamsDto);
    }

    private static T FetchCollection<T>(string jsonResponse, string collectionName)
    {
        // Parse the JSON response
        var jsonObject = JObject.Parse(jsonResponse);

        // Extract the desired collection and deserialize it
        var collection = jsonObject[collectionName].ToObject<T>();

        return collection;
    }


    private static T FetchData<T>(string jsonResponse) => JsonConvert.DeserializeObject<T>(jsonResponse);
}