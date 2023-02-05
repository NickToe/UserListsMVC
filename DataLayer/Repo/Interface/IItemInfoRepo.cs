namespace UserListsMVC.DataLayer.Repo.Interface;

public interface IItemInfoRepo
{
  public Task<bool> Any(string itemId);
  public Task<ItemInfo> Get(string itemId);
  public Task Add(ItemInfo itemInfo);
  public Task UpdateViewCounter(string itemId, string userId);
  public Task Save();
}
