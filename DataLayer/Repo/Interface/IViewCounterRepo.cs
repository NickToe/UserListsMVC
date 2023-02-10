namespace UserListsMVC.DataLayer.Repo.Interface;

public interface IViewCounterRepo
{
    public Task<bool> Any(string userId, int itemInfoId);
    public Task<int> GetCounter(int itemInfoId);
    public Task<ViewCounter> Get(int itemInfoId);
    public Task Save();
}
