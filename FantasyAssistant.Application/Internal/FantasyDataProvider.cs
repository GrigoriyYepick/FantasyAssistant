using AutoMapper;
using FantasyAssistant.Application.Model;
using FantasyAssistant.Transport;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FantasyAssistant.Application.Internal;

internal sealed class FantasyDataProvider : IFantasyDataProvider
{
    private readonly IMapper _mapper;
    private readonly IFantasyApiService _fantasyApiService;
    private readonly FantasySettings _fantasySettings;
    private readonly ApiSettings _apiSettings;

    public FantasyDataProvider(
        IMapper mapper,
        IFantasyApiService fantasyApiService,
        IOptions<ApiSettings> apiSettings,
        IOptions<FantasySettings> fantasySettings)
    {
        _mapper = mapper;
        _fantasyApiService = fantasyApiService;
        _fantasySettings = fantasySettings.Value;
        _apiSettings = apiSettings.Value;
    }

    public async Task<FantasyRawData> ReadRawDataAsync()
    {
        var bootstrapStats = await _fantasyApiService.GetDataFromRouteAsync(_apiSettings.Bootstrap).ConfigureAwait(false);

        var fixtures = await ReadFixtures().ConfigureAwait(false);
        var allPlayers = ReadAllPlayers(bootstrapStats);
        var teams = ReadTeams(bootstrapStats);
        var playersPicks = await ReadMyPlayers().ConfigureAwait(false);

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

    private async Task<IReadOnlyCollection<PlayerPick>> ReadMyPlayers()
    {
        var myTeamUri = string.Format(_apiSettings.Team, _fantasySettings.TeamId, _fantasySettings.LastGameWeek);
        var response = await _fantasyApiService.GetDataFromRouteAsync(myTeamUri).ConfigureAwait(false);
        var playersPicksDto = FetchCollection<List<PickDto>>(response, "picks");
        return _mapper.Map<IReadOnlyCollection<PlayerPick>>(playersPicksDto);
    }

    private async Task<IReadOnlyCollection<Fixture>> ReadFixtures()
    {
        var response = await _fantasyApiService.GetDataFromRouteAsync(_apiSettings.Fixtures).ConfigureAwait(false);
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