using UserListsMVC.Json;

namespace UserListsMVC.DataLayer.Models;

public record Movie
{
  public string Id { get; set; } = null!;
  public string Title { get; set; } = null!;
  public string? FullTitle { get; set; }
  public string? Type { get; set; }
  public string? Year { get; set; }
  public string PosterLink { get; set; } = null!;
  public string? ReleaseDate { get; set; }
  public string? RuntimeMins { get; set; }
  public string? RuntimeStr { get; set; }
  public string? Plot { get; set; }
  public string? Directors { get; set; }
  public string? Stars { get; set; }
  public string? Genres { get; set; }
  public string? Companies { get; set; }
  public string? Countries { get; set; }
  public string? ContentRating { get; set; }
  public string? ImdbRating { get; set; }
  public string? ImdbRatingVotes { get; set; }
  public string? MetascriticRating { get; set; }

  public static Movie JsonToModel(MovieJson movieJson) =>
    new Movie()
    {
      Id = movieJson.Id,
      Title= movieJson.Title,
      FullTitle= movieJson.FullTitle,
      Year = movieJson.Year,
      Type = movieJson.Type,
      PosterLink = movieJson.PosterLink,
      ReleaseDate= movieJson.ReleaseDate,
      RuntimeMins= movieJson.RuntimeMins,
      RuntimeStr= movieJson.RuntimeStr,
      Plot = movieJson.Plot,
      Directors= movieJson.Directors,
      Stars= movieJson.Stars,
      Genres= movieJson.Genres,
      Companies = movieJson.Companies,
      Countries = movieJson.Countries,
      ContentRating= movieJson.ContentRating,
      ImdbRating= movieJson.ImdbRating,
      ImdbRatingVotes= movieJson.ImdbRatingVotes,
      MetascriticRating= movieJson.MetascriticRating,
    };
}
