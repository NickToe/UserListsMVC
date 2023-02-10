using System.Text;
using UserListsMVC.Json;

namespace UserListsMVC.ApiLayer;

public class WebApiGame : WebApiBase<GameJson>, IWebApi<Game>
{
    private readonly ILogger<WebApiGame> _logger;
    public WebApiGame(ILogger<WebApiGame> logger, IConfiguration configuration) : base(logger, configuration, "api/Game/")
    {
        _logger = logger;
    }

    public async Task<Game> GetItemById(string id)
    {
        UriBuilder uriBuilder = CopyUriBuilder();
        uriBuilder.Path = GetFullPath("ById");
        uriBuilder.Query = $"id={id}";
        GameJson gameJson = await GetJsonItem(uriBuilder.Uri);
        return Game.JsonToModel(gameJson);
    }

    public async Task<IEnumerable<Game>> GetItemsByIds(IEnumerable<string> ids)
    {
        UriBuilder uriBuilder = CopyUriBuilder();
        uriBuilder.Path = GetFullPath("ByIds");
        StringBuilder stringBuilder = new StringBuilder();
        ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
        uriBuilder.Query = stringBuilder.ToString();
        ICollection<Game> games = new List<Game>();
        (await GetJsonItems(uriBuilder.Uri)).ToList().ForEach(item => games.Add(Game.JsonToModel(item)));
        return games;
    }

    public async Task<IEnumerable<Game>> GetItemsByTitle(string title)
    {
        UriBuilder uriBuilder = CopyUriBuilder();
        uriBuilder.Path = GetFullPath("ByTitle");
        uriBuilder.Query = $"title={title}";
        ICollection<Game> games = new List<Game>();
        (await GetJsonItems(uriBuilder.Uri)).ToList().ForEach(item => games.Add(Game.JsonToModel(item)));
        return games;
    }
}