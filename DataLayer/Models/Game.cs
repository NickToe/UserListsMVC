using UserListsMVC.Json;

namespace UserListsMVC.DataLayer.Models;

public record Game
{
  public string Id { get; set; } = null!;
  public string Title { get; set; } = null!;
  public List<string>? Developers { get; set; }
  public List<string>? Genres { get; set; }
  public string? PosterLink { get; set; }
  public short? MetacriticScore { get; set; }
  public string? MetacriticUrl { get; set; }
  public List<string>? Publishers { get; set; }
  public bool? ComingSoon { get; set; }
  public string? ReleaseDate { get; set; }
  public string ShortDescription { get; set; } = null!;
  public string Type { get; set; } = null!;
  public string? ReviewScore { get; set; }
  public int? TotalPositive { get; set; }
  public int? TotalNegative { get; set; }
  public int? TotalReviews { get; set; }

  public static Game JsonToModel(GameJson gameJson) => new()
  {
    Id = gameJson.Id,
    Title = gameJson.Title,
    Type = gameJson.Type,
    PosterLink = gameJson.Poster,
    ShortDescription = gameJson.ShortDescription,
    Genres = gameJson?.Genres?.ToList(),
    Developers = gameJson?.Developers?.ToList(),
    Publishers = gameJson?.Publishers?.ToList(),
    ComingSoon = gameJson?.ComingSoon,
    ReleaseDate = gameJson?.ReleaseDate,
    MetacriticScore = gameJson?.MetacriticScore,
    MetacriticUrl = gameJson?.MetacriticUrl,
    ReviewScore = gameJson?.ReviewScore,
    TotalPositive = gameJson?.TotalPositive,
    TotalNegative = gameJson?.TotalNegative,
    TotalReviews = gameJson?.TotalReviews,
  };
}