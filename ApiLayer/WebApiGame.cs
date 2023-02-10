using System.Text;
using AutoMapper;
using UserListsMVC.Json;

namespace UserListsMVC.ApiLayer;

public class WebApiGame : WebApiBase<GameJson>, IWebApi<Game>
{
    private readonly ILogger<WebApiGame> _logger;
    public WebApiGame(ILogger<WebApiGame> logger, IConfiguration configuration, IMapper mapper) : base(logger, configuration, mapper)
    {
        _logger = logger;
    }

    public async Task<Game> GetItemById(string id)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"game/{id}");
        GameJson gameJson = await GetJsonItem(uriBuilder.Uri);
        return _mapper.Map<Game>(gameJson);
    }

    public async Task<IEnumerable<Game>> GetItemsByIds(IEnumerable<string> ids)
    {
        UriBuilder uriBuilder = CopyUriBuilder("games");
        StringBuilder stringBuilder = new StringBuilder();
        ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
        uriBuilder.Query = stringBuilder.ToString();
        IEnumerable<GameJson> games = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<Game>>(games);
    }

    public async Task<IEnumerable<Game>> GetItemsByTitle(string title)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"games/title");
        uriBuilder.Query = $"title={title}";
        IEnumerable<GameJson> games = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<Game>>(games);
    }
}