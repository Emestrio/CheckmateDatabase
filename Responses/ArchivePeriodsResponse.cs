using System.Text.Json.Serialization;

internal readonly record struct ArchivePeriodsResponse
{
    [JsonPropertyName("archives")]
    public required string[] Archives { get; init; }
}