namespace UserListsMVC.DataLayer.Repo.Interface;

public interface IVoteRepo<T> where T : class
{
    public Task<bool> Any(string userId, int parentId);
    public Task Add(T vote);
    public Task<T> Get(string userId, int parentId);
    public Task Save();
}
