using UserListsMVC.Json;

namespace UserListsMVC.DataLayer.Models;

public record GameGenre
{
  public short GenreId { get; set; }
  public string? Description { get; set; }

  public GameGenre(short genreId, string? description)
  {
    GenreId = genreId;
    Description = description;
  }

  public static GameGenre JsonToModel(GameGenreJson genre) => new(genre.GenreId, genre.Description);

  public static IEnumerable<GameGenre> JsonsToModels(IEnumerable<GameGenreJson> genres)
  {
    ICollection<GameGenre> tmpGenres = new List<GameGenre>();
    foreach (var genre in genres) tmpGenres.Add(JsonToModel(genre));
    return tmpGenres;
  }
}
