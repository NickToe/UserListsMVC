namespace UserListsMVC.ServiceLayer;

public interface IItemService<T>
{
  public Task<IEnumerable<T>> GetByName(string title);
  public Task<IEnumerable<T>> GetByIds(IEnumerable<string> ids);
}