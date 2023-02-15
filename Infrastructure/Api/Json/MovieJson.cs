using System.Text.Json.Serialization;

namespace UserListsMVC.Infrastructure.Api.Json;

public record MovieJson
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("fullTitle")]
    public string? FullTitle { get; set; }

    [JsonPropertyName("type")]
    public string? Type { get; set; }

    [JsonPropertyName("year")]
    public string? Year { get; set; }

    [JsonPropertyName("poster")]
    public string Poster { get; set; } = null!;

    [JsonPropertyName("releaseDate")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("runtimeMins")]
    public string? RuntimeMins { get; set; }

    [JsonPropertyName("runtimeStr")]
    public string? RuntimeStr { get; set; }

    [JsonPropertyName("plot")]
    public string? Plot { get; set; }

    [JsonPropertyName("directors")]
    public string? Directors { get; set; }

    [JsonPropertyName("stars")]
    public string? Stars { get; set; }

    [JsonPropertyName("genres")]
    public string? Genres { get; set; }

    [JsonPropertyName("companies")]
    public string? Companies { get; set; }

    [JsonPropertyName("countries")]
    public string? Countries { get; set; }

    [JsonPropertyName("contentRating")]
    public string? ContentRating { get; set; }

    [JsonPropertyName("imdbRating")]
    public string? ImdbRating { get; set; }

    [JsonPropertyName("imdbRatingVotes")]
    public string? ImdbRatingVotes { get; set; }

    [JsonPropertyName("metascriticRating")]
    public string? MetascriticRating { get; set; }
}
