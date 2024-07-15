using System.Net.Http.Json;

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

        foreach (var url in respons!.Archives)
        {
            yield return new Uri(url);
        }
    }

    internal static async IAsyncEnumerable<Game> GetAllNormalGames(Uri url)
    {
        var respons = await httpClient.GetFromJsonAsync<GameResponse>(url);

        foreach (var game in respons!.Games)
        {
            if (IsGameRankedChess(game))
            {
                yield return game;
            }
        }
    }

    private static bool IsGameRankedChess(Game game)
    {
        return game.Rated == true && game.Rules == Ruleset.Chess;
    }
}