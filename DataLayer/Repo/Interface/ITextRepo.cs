namespace UserListsMVC.DataLayer.Repo.Interface;

public interface ITextRepo<T>
{
    public Task Add(T text);
    public void Remove(T text);
    public Task<T> Get(int textId);
    public Task Save();
}