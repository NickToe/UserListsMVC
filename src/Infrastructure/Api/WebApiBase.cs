using System.Net.Http.Json;
using AutoMapper;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Api;

public abstract class WebApiBase<JsonModel> where JsonModel : class, new()
{
    private readonly ILogger<WebApiBase<JsonModel>> _logger;
    protected readonly IMapper _mapper;
    protected readonly HttpClient _httpClient = new();
    protected readonly string _apiVersion;
    protected readonly UriBuilder _uriBuilder;

    public WebApiBase(ILogger<WebApiBase<JsonModel>> logger, IConfiguration configuration, IMapper mapper)
    {
        _logger = logger;
        _mapper = mapper;
        _apiVersion = configuration.GetValue<string>("UserListsApi:ApiVersion");

        _uriBuilder = new UriBuilder()
        {
            Scheme = "https",
            Host = configuration.GetValue<string>("UserListsApi:Host"),
            Port = configuration.GetValue<int>("UserListsApi:Port"),
            Path = $"api/{_apiVersion}"
        };

        _httpClient.DefaultRequestHeaders.Add("XApiKey", configuration.GetValue<string>("UserListsApi:ApiKey"));
    }

    protected UriBuilder CopyUriBuilder(string pathAppend) => new UriBuilder()
    {
        Scheme = _uriBuilder.Scheme,
        Host = _uriBuilder.Host,
        Port = _uriBuilder.Port,
        Path = $"{_uriBuilder.Path}/{pathAppend}",
    };

    protected async Task<JsonModel> GetJsonItem(Uri uri)
    {
        _logger.LogInformation("GetJsonItem(): Request URL: {url}", uri);
        JsonModel model;
        try
        {
            model = await _httpClient.GetFromJsonAsync<JsonModel>(uri) ?? throw new Exception($"Unable to get item for this request {uri}");
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception generated while making a request: {message}", ex.Message);
            model = new JsonModel();
        }
        return model;
    }

    protected async Task<IEnumerable<JsonModel>> GetJsonItems(Uri uri)
    {
        _logger.LogInformation("GetJsonItems(): Request URL: {url}", uri);
        IEnumerable<JsonModel> models;
        try
        {
            models = await _httpClient.GetFromJsonAsync<IEnumerable<JsonModel>>(uri) ?? Enumerable.Empty<JsonModel>();
        }
        catch (Exception ex)
        {
            _logger.LogError("Exception generated while making a request: {message}", ex.Message);
            models = Enumerable.Empty<JsonModel>();
        }
        return models;
    }
}