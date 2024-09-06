namespace FantasyAssistant.Application.Internal;

internal interface IFantasyApiService
{
    Task<string> GetDataFromRouteAsync(string route);
}