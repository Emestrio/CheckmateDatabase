using System.Text.Json;
using System.Text.Json.Serialization;

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
    public required string Time_control { get; init; }
    [JsonPropertyName("end_time")]
    public required uint End_time { get; init; }
    [JsonPropertyName("rated")]
    public required bool Rated { get; init; }
    [JsonPropertyName("tcn")]
    public string Tcn { get; init; }
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
    Move50,
    Abandoned,
    Kingofthehill,
    Threecheck,
    Bughousepartnerlose,
    TimeVsInsufficient,
}

internal class CustomEnumConverter : JsonConverter<Result>
{
    public override Result Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var str = reader.GetString()?.Trim();

        return str switch
        {
            "win" => Result.Win,
            "checkmated" => Result.Checkmated,
            "agreed" => Result.Agreed,
            "repetition" => Result.Repetition,
            "timeout" => Result.Timeout,
            "resigned" => Result.Resigned,
            "stalemate" => Result.Stalemate,
            "lose" => Result.Lose,
            "insufficient" => Result.Insufficient,
            "50move" => Result.Move50,
            "abandoned" => Result.Abandoned,
            "kingofthehill" => Result.Kingofthehill,
            "threecheck" => Result.Threecheck,
            "bughousepartnerlose" => Result.Bughousepartnerlose,
            "timevsinsufficient" => Result.TimeVsInsufficient,
            var value => throw new JsonException($"value {value} have no corrosponding {nameof(Result)} enum")
        };
    }

    public override void Write(Utf8JsonWriter writer, Result value, JsonSerializerOptions options)
    {
        string str = value switch
        {
            Result.Move50 => "50move",
            var a => a.ToString().ToLower(),
        };

        writer.WriteStringValue(str);
    }
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Ruleset
{
    Chess,
    Chess960,
}