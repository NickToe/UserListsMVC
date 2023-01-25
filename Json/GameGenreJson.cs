using System.Text.Json.Serialization;

namespace UserListsMVC.Json;

public record GameGenreJson
{
  [JsonPropertyName("genreId")]
  public short GenreId { get; set; }

  [JsonPropertyName("description")]
  public string? Description { get; set; }
}