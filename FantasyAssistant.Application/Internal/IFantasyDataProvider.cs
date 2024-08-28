namespace FantasyAssistant.Application.Internal;

internal interface IFantasyDataProvider
{
    Task<FantasyRawData> ReadRawDataAsync();
}