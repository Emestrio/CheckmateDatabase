using System.Net.Http.Json;
using System.Runtime.Serialization;
using System.Text.Json;
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
            yield return game;
        }
    }
}

internal readonly record struct ArchivePeriodsResponse
{
    [JsonPropertyName("archives")]
    public required string[] Archives { get; init; }
}

internal readonly record struct GameResponse
{
    [JsonPropertyName("games")]
    public required Game[] Games { get; init; }
}

internal readonly record struct Game
{
    [JsonPropertyName("url")]
    public required string Url { get; init; }
    [JsonPropertyName("pgn")]
    public required string Pgn { get; init; }
    [JsonPropertyName("time_control")]
    public required uint Time_control { get; init; }
    [JsonPropertyName("end_time")]
    public required uint End_time { get; init; }
    [JsonPropertyName("rated")]
    public required bool Rated { get; init; }
    [JsonPropertyName("tcn")]
    public required string Tcn { get; init; }
    [JsonPropertyName("uuid")]
    public required Guid Uuid { get; init; }
    [JsonPropertyName("initial_setup")]
    public required string Initial_setup { get; init; }
    [JsonPropertyName("fen")]
    public required string Fen { get; init; }
    [JsonPropertyName("time_class")]
    public required string Time_class { get; init; }
    [JsonPropertyName("rules")]
    public required Ruleset Rules { get; init; }
    [JsonPropertyName("black")]
    public required Player Black { get; init; }
    [JsonPropertyName("white")]
    public required Player White { get; init; }

}

internal readonly record struct Player
{
    [JsonPropertyName("rating")]
    public required ushort Rating { get; init; }
    [JsonPropertyName("result")]
    public required Result Result { get; init; }
    [JsonPropertyName("@id")]
    public required string ID { get; init; }
    [JsonPropertyName("username")]
    public required string Username { get; init; }
    [JsonPropertyName("uuid")]
    public required Guid Uuid { get; init; }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChessColor
{
    Black,
    White,
}

[JsonConverter(typeof(CustomEnumConverter))]
public enum Result
{
    Win,
    Checkmated,
    Agreed,
    Repetition,
    Timeout,
    Resigned,
    Stalemate,
    Lose,
    Insufficient,
    [EnumMember(Value = "50move")]
    Move50,
    Abandoned,
    Kingofthehill,
    Threecheck,
    Bughousepartnerlose,
}

internal class CustomEnumConverter : JsonConverter<Result>
{
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        throw new NotImplementedException();
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Ruleset
{
    Chess,
    Chess960,
}