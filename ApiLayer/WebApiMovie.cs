using System.Text;
using UserListsMVC.DataLayer.Models;
using UserListsMVC.Json;

namespace UserListsMVC.ApiLayer;

public class WebApiMovie : WebApiBase<MovieJson>, IWebApi<Movie>
{
  private readonly ILogger<WebApiMovie> _logger;
  public WebApiMovie(ILogger<WebApiMovie> logger, IConfiguration configuration) : base(logger, configuration, "api/Movie/")
  {
    _logger = logger;
  }

  public async Task<Movie> GetItemById(string id)
  {
    UriBuilder uriBuilder = CopyUriBuilder();
    uriBuilder.Path = GetFullPath("ById");
    uriBuilder.Query = $"id={id}";
    MovieJson movieJson = await GetJsonItem(_uriBuilder.Uri);
    return Movie.JsonToModel(movieJson);
  }

  public async Task<IEnumerable<Movie>> GetItemsByIds(IEnumerable<string> ids)
  {
    UriBuilder uriBuilder = CopyUriBuilder();
    uriBuilder.Path = GetFullPath("ByIds");
    StringBuilder stringBuilder = new StringBuilder();
    ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
    uriBuilder.Query = stringBuilder.ToString();
    ICollection<Movie> movies = new List<Movie>();
    (await GetJsonItems(uriBuilder.Uri)).ToList().ForEach(item => movies.Add(Movie.JsonToModel(item)));
    return movies;
  }

  public async Task<IEnumerable<Movie>> GetItemsByTitle(string title)
  {
    UriBuilder uriBuilder = CopyUriBuilder();
    uriBuilder.Path = GetFullPath("ByTitle");
    uriBuilder.Query = $"title={title}";
    ICollection<Movie> movies = new List<Movie>();
    (await GetJsonItems(uriBuilder.Uri)).ToList().ForEach(item => movies.Add(Movie.JsonToModel(item)));
    return movies;
  }
}
