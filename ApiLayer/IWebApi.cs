namespace UserListsMVC.ApiLayer;

public interface IWebApi<T>
{
    public Task<T> GetItemById(string id);
    public Task<IEnumerable<T>> GetItemsByIds(IEnumerable<string> ids);
    public Task<IEnumerable<T>> GetItemsByTitle(string name);
}