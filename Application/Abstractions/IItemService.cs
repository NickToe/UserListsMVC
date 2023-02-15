namespace UserListsMVC.Application.Abstractions;

public interface IItemService<T>
{
    public Task<T> GetById(string id);
    public Task<IEnumerable<T>> GetByName(string title);
    public Task<IEnumerable<T>> GetByIds(IEnumerable<string> ids);
}