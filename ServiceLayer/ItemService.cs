using UserListsMVC.ApiLayer;

namespace UserListsMVC.ServiceLayer;

public class ItemService<T> : IItemService<T>
{
  private readonly IWebApi<T> _webApi;
  public ItemService(IWebApi<T> webApi)
  {
    _webApi = webApi;
  }

  public async Task<IEnumerable<T>> GetByName(string title) => await _webApi.GetItemsByTitle(title);
  public async Task<IEnumerable<T>> GetByIds(IEnumerable<string> ids) => await _webApi.GetItemsByIds(ids);
}