
using System.Net.Http.Json;
using System.Text.Json.Serialization;

internal static class ChessDotComHttpClient
{
    private static readonly HttpClient httpClient = new();

    static ChessDotComHttpClient()
    {
        httpClient.DefaultRequestHeaders.UserAgent.ParseAdd("Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/126.0.0.0 Safari/537.36");
    }

    internal static async IAsyncEnumerable<Uri> GetArchivePeriods(string username)
    {
        var respons = await httpClient.GetFromJsonAsync<ArchivePeriodsResponse>($"https://api.chess.com/pub/player/{username}/games/archives");

        foreach (var item in respons!.Archives)
        {
            yield return new Uri(item);
        }
    }
}

internal readonly record struct ArchivePeriodsResponse
{
    [JsonPropertyName("archives")]
    public required string[] Archives { get; init; }
}