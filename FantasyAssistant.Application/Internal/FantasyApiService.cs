namespace FantasyAssistant.Application.Internal;

internal sealed class FantasyApiService : IFantasyApiService
{
    private readonly HttpClient _httpClient;

    public FantasyApiService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("api");
    }

    public async Task<string> GetDataFromRouteAsync(string route)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(route, nameof(route));
        return await _httpClient.GetStringAsync(route);
    }
}