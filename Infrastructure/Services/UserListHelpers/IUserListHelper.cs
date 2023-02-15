using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Infrastructure.Services.UserListHelpers;

public interface IUserListHelper<T> where T : UserListItemBase
{
    public Task<UserList<T>> GetWithItems(string userName, string listName);
    public Task<UserList<T>> Get(string userName, string listName);
    public Task<bool> AddItem(string userName, string listName, T item);
    public Task<bool> RemoveItem(string userName, string listName, string itemId);
    public Task<bool> UpdateItem(string userName, string listName, T item);
    public Task<bool> UpdatePrivacy(string userName, string listName, bool isPublic);
    public Task<bool> IsListPresent(string userName, string listName);
    public Task<bool> AddList(string userName, string listName, ContentType contentType, UserListType userListType);
    public Task<bool> RemoveList(string userName, string listName);
    public Task<ISet<UserList<T>>> GetLists(string userName);
}