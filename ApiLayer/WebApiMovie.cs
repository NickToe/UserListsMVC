using System.Text;
using AutoMapper;
using UserListsMVC.Json;

namespace UserListsMVC.ApiLayer;

public class WebApiMovie : WebApiBase<MovieJson>, IWebApi<Movie>
{
    private readonly ILogger<WebApiMovie> _logger;
    public WebApiMovie(ILogger<WebApiMovie> logger, IConfiguration configuration, IMapper mapper) : base(logger, configuration, mapper)
    {
        _logger = logger;
    }

    public async Task<Movie> GetItemById(string id)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"movie/{id}");
        MovieJson movieJson = await GetJsonItem(uriBuilder.Uri);
        return _mapper.Map<Movie>(movieJson);
    }

    public async Task<IEnumerable<Movie>> GetItemsByIds(IEnumerable<string> ids)
    {
        UriBuilder uriBuilder = CopyUriBuilder("moves");
        StringBuilder stringBuilder = new StringBuilder();
        ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
        uriBuilder.Query = stringBuilder.ToString();
        IEnumerable<MovieJson> movies = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }

    public async Task<IEnumerable<Movie>> GetItemsByTitle(string title)
    {
        UriBuilder uriBuilder = CopyUriBuilder("movies/title");
        uriBuilder.Query = $"title={title}";
        IEnumerable<MovieJson> movies = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<Movie>>(movies);
    }
}
