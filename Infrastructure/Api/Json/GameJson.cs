using System.Text.Json.Serialization;

namespace UserListsMVC.Infrastructure.Api.Json;

public record GameJson
{
    [JsonPropertyName("id")]
    public string Id { get; set; } = null!;

    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;

    [JsonPropertyName("type")]
    public string Type { get; set; } = null!;

    [JsonPropertyName("poster")]
    public string Poster { get; set; } = null!;

    [JsonPropertyName("shortDescription")]
    public string ShortDescription { get; set; } = null!;

    [JsonPropertyName("genres")]
    public ICollection<string>? Genres { get; set; }

    [JsonPropertyName("developers")]
    public ICollection<string>? Developers { get; set; }

    [JsonPropertyName("publishers")]
    public ICollection<string>? Publishers { get; set; }

    [JsonPropertyName("comingSoon")]
    public bool? ComingSoon { get; set; }

    [JsonPropertyName("releaseDate")]
    public string? ReleaseDate { get; set; }

    [JsonPropertyName("metacriticUrl")]
    public string? MetacriticUrl { get; set; }

    [JsonPropertyName("metacriticScore")]
    public short? MetacriticScore { get; set; }

    [JsonPropertyName("reviewScore")]
    public string? ReviewScore { get; set; }

    [JsonPropertyName("totalPositive")]
    public int? TotalPositive { get; set; }

    [JsonPropertyName("totalNegative")]
    public int? TotalNegative { get; set; }

    [JsonPropertyName("totalReviews")]
    public int? TotalReviews { get; set; }
}