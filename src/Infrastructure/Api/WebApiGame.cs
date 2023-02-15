using System.Text;
using Application.DTOs;
using AutoMapper;
using Infrastructure.Api.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Api;

public class WebApiGame : WebApiBase<GameJson>, IWebApi<GameDTO>
{
    private readonly ILogger<WebApiGame> _logger;
    public WebApiGame(ILogger<WebApiGame> logger, IConfiguration configuration, IMapper mapper) : base(logger, configuration, mapper)
    {
        _logger = logger;
    }

    public async Task<GameDTO> GetItemById(string id)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"game/{id}");
        GameJson gameJson = await GetJsonItem(uriBuilder.Uri);
        return _mapper.Map<GameDTO>(gameJson);
    }

    public async Task<IEnumerable<GameDTO>> GetItemsByIds(IEnumerable<string> ids)
    {
        UriBuilder uriBuilder = CopyUriBuilder("games");
        StringBuilder stringBuilder = new StringBuilder();
        ids.ToList().ForEach(item => stringBuilder.AppendJoin(item, "ids=", "&"));
        uriBuilder.Query = stringBuilder.ToString();
        IEnumerable<GameJson> games = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<GameDTO>>(games);
    }

    public async Task<IEnumerable<GameDTO>> GetItemsByTitle(string title)
    {
        UriBuilder uriBuilder = CopyUriBuilder($"games/title");
        uriBuilder.Query = $"title={title}";
        IEnumerable<GameJson> games = await GetJsonItems(uriBuilder.Uri);
        return _mapper.Map<IEnumerable<GameDTO>>(games);
    }
}