using UserListsMVC.Json;

namespace UserListsMVC.DataLayer.Models;

public record Movie
{
  public string Id { get; set; } = null!;
  public string Title { get; set; } = null!;
  public string FullTitle { get; set; } = String.Empty;
  public string Type { get; set; } = String.Empty;
  public string Year { get; set; } = String.Empty;
  public string PosterLink { get; set; } = null!;
  public string ReleaseDate { get; set; } = String.Empty;
  public string RuntimeMins { get; set; } = String.Empty;
  public string RuntimeStr { get; set; } = String.Empty;
  public string Plot { get; set; } = String.Empty;
  public string Directors { get; set; } = String.Empty;
  public string Stars { get; set; } = String.Empty;
  public string Genres { get; set; } = String.Empty;
  public string Companies { get; set; } = String.Empty;
  public string Countries { get; set; } = String.Empty;
  public string ContentRating { get; set; } = String.Empty;
  public string ImdbRating { get; set; } = String.Empty;
  public string ImdbRatingVotes { get; set; } = String.Empty;
  public string MetascriticRating { get; set; } = String.Empty;

  public static Movie JsonToModel(MovieJson movieJson) => new()
  {
    Id = movieJson.Id,
    Title = movieJson.Title,
    FullTitle = movieJson?.FullTitle ?? String.Empty,
    Year = movieJson?.Year ?? String.Empty,
    Type = movieJson?.Type ?? String.Empty,
    PosterLink = movieJson?.Poster ?? String.Empty,
    ReleaseDate = movieJson?.ReleaseDate ?? String.Empty,
    RuntimeMins = movieJson?.RuntimeMins ?? String.Empty,
    RuntimeStr = movieJson?.RuntimeStr ?? String.Empty,
    Plot = movieJson?.Plot ?? String.Empty,
    Directors = movieJson?.Directors ?? String.Empty,
    Stars = movieJson?.Stars ?? String.Empty,
    Genres = movieJson?.Genres ?? String.Empty,
    Companies = movieJson?.Companies ?? String.Empty,
    Countries = movieJson?.Countries ?? String.Empty,
    ContentRating = movieJson?.ContentRating ?? String.Empty,
    ImdbRating = movieJson?.ImdbRating ?? String.Empty,
    ImdbRatingVotes = movieJson?.ImdbRatingVotes ?? String.Empty,
    MetascriticRating = movieJson?.MetascriticRating ?? String.Empty,
  };
}