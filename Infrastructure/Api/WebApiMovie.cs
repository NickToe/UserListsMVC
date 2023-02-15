using System.Text;
using AutoMapper;
using UserListsMVC.Application.DTOs;
using UserListsMVC.Infrastructure.Api.Json;

namespace UserListsMVC.Infrastructure.Api;

public class WebApiMovie : WebApiBase<MovieJson>, IWebApi<MovieDTO>
{
    private readonly ILogger<WebApiMovie> _logger;
    public WebApiMovie(ILogger<WebApiMovie> logger, IConfiguration configuration, IMapper mapper) : base(logger, configuration, mapper)
    {
        _logger = logger;
    }

    public async Task<MovieDTO> GetItemById(string id)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"movie/{id}");
        MovieJson movieJson = await GetJsonItem(uriBuilder.Uri);
        return _mapper.Map<MovieDTO>(movieJson);
    }

    public async Task<IEnumerable<MovieDTO>> GetItemsByIds(IEnumerable<string> ids)
    {
        UriBuilder uriBuilder = CopyUriBuilder("moves");
        StringBuilder stringBuilder = new StringBuilder();
        ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
        uriBuilder.Query = stringBuilder.ToString();
        IEnumerable<MovieJson> movies = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<MovieDTO>>(movies);
    }

    public async Task<IEnumerable<MovieDTO>> GetItemsByTitle(string title)
    {
        UriBuilder uriBuilder = CopyUriBuilder("movies/title");
        uriBuilder.Query = $"title={title}";
        IEnumerable<MovieJson> movies = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<MovieDTO>>(movies);
    }
}
