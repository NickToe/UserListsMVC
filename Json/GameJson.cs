using System.Text.Json.Serialization;
using UserListsMVC.Json;

namespace UserListsMVC.Json;

public record GameJson
{
  [JsonPropertyName("id")]
  public int Id { get; set; }

  [JsonPropertyName("title")]
  public string Title { get; set; } = null!;

  [JsonPropertyName("type")]
  public string Type { get; set; } = null!;

  [JsonPropertyName("headerImage")]
  public string PosterLink { get; set; } = null!;

  [JsonPropertyName("shortDescription")]
  public string ShortDescription { get; set; } = null!;

  [JsonPropertyName("genres")]
  public ICollection<GameGenreJson>? Genres { get; set; }

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