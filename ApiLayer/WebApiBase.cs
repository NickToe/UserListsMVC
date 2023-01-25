namespace UserListsMVC.ApiLayer;

public abstract class WebApiBase<JsonModel>
{
  private readonly ILogger<WebApiBase<JsonModel>> _logger;
  protected readonly HttpClient _httpClient = new();
  protected readonly string _resourcePath;
  protected readonly UriBuilder _uriBuilder;

  public WebApiBase(ILogger<WebApiBase<JsonModel>> logger, IConfiguration configuration, string resourcePath)
  {
    _uriBuilder = new UriBuilder()
    {
      Scheme = "https",
      Host = configuration.GetValue<string>("PortalWebApi:Host"),
      Port = configuration.GetValue<int>("PortalWebApi:Port")
    };
    _resourcePath = resourcePath;
    _logger = logger;
  }

  protected string GetFullPath(string path) => _resourcePath + path;

  protected UriBuilder CopyUriBuilder() => new UriBuilder()
  {
    Scheme = _uriBuilder.Scheme,
    Host = _uriBuilder.Host,
    Port = _uriBuilder.Port,
  };

  protected async Task<JsonModel> GetJsonItem(Uri uri)
  {
    _logger.LogInformation("GetJsonItem(): Request URL: {url}", uri);
    return await _httpClient.GetFromJsonAsync<JsonModel>(uri) ?? throw new Exception($"Unable to get item for this request {uri}");
  }

  protected async Task<IEnumerable<JsonModel>> GetJsonItems(Uri uri)
  {
    _logger.LogInformation("GetJsonItems(): Request URL: {url}", uri);
    return await _httpClient.GetFromJsonAsync<IEnumerable<JsonModel>>(uri) ?? Enumerable.Empty<JsonModel>();
  }
}