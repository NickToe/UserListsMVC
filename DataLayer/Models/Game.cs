using UserListsMVC.Json;

namespace UserListsMVC.DataLayer.Models;

public record Game
{
  public string Id { get; set; } = null!;
  public string Title { get; set; } = null!;
  public List<string> Developers { get; set; } = new List<string>();
  public List<string> Genres { get; set; } = new List<string>();
  public string PosterLink { get; set; } = string.Empty!;
  public short MetacriticScore { get; set; }
  public string MetacriticUrl { get; set; } = string.Empty!;
  public List<string> Publishers { get; set; } = new List<string>();
  public bool ComingSoon { get; set; }
  public string ReleaseDate { get; set; } = string.Empty!;
  public string ShortDescription { get; set; } = null!;
  public string Type { get; set; } = null!;
  public string ReviewScore { get; set; } = string.Empty!;
  public int TotalPositive { get; set; }
  public int TotalNegative { get; set; }
  public int TotalReviews { get; set; }

  public static Game JsonToModel(GameJson gameJson) => new()
  {
    Id = gameJson.Id,
    Title = gameJson.Title,
    Type = gameJson.Type,
    PosterLink = gameJson.Poster,
    ShortDescription = gameJson.ShortDescription,
    Genres = gameJson?.Genres?.ToList() ?? new List<string>(),
    Developers = gameJson?.Developers?.ToList() ?? new List<string>(),
    Publishers = gameJson?.Publishers?.ToList() ?? new List<string>(),
    ComingSoon = gameJson?.ComingSoon ?? false,
    ReleaseDate = gameJson?.ReleaseDate ?? String.Empty,
    MetacriticScore = gameJson?.MetacriticScore ?? 0,
    MetacriticUrl = gameJson?.MetacriticUrl ?? String.Empty,
    ReviewScore = gameJson?.ReviewScore ?? String.Empty,
    TotalPositive = gameJson?.TotalPositive ?? 0,
    TotalNegative = gameJson?.TotalNegative ?? 0,
    TotalReviews = gameJson?.TotalReviews ?? 0,
  };
}