using System.Security.Claims;

namespace UserListsMVC.ServiceLayer.Interface;

public interface IUserListService<T> where T : UserListItemBase
{
    public Task<UserList<T>> Get(ClaimsPrincipal? claimsUser, string listName);
    public Task<UserList<T>> Get(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType);
    public Task<UserList<T>> Get(string userName, ContentType contentType, UserListType userListType);
    public Task<UserList<T>> Get(string userName, string listName);

    public Task Add(ClaimsPrincipal? claimsUser, string listName, T item);
    public Task Add(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, T item);
    public Task Add(string userName, ContentType contentType, UserListType userListType, T item);
    public Task Add(string userName, string listName, T item);

    public Task Remove(ClaimsPrincipal? claimsUser, string listName, string itemId);
    public Task Remove(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, string itemId);
    public Task Remove(string userName, ContentType contentType, UserListType userListType, string itemId);
    public Task Remove(string userName, string listName, string itemId);

    public Task Update(ClaimsPrincipal? claimsUser, string listName, T item);
    public Task Update(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, T item);
    public Task Update(string userName, ContentType contentType, UserListType userListType, T item);
    public Task Update(string userName, string listName, T item);

    public Task UpdatePrivacy(ClaimsPrincipal? claimsUser, string listName, bool isPublic);
    public Task UpdatePrivacy(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, bool isPublic);
    public Task UpdatePrivacy(string userName, ContentType contentType, UserListType userListType, bool isPublic);
    public Task UpdatePrivacy(string userName, string listName, bool isPublic);

    public Task AddList(ClaimsPrincipal? claimsUser, string listName, ContentType contentType, UserListType userListType);
    public Task AddList(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType);
    public Task AddList(string userName, ContentType contentType, UserListType userListType);
    public Task AddList(string userName, string listName, ContentType contentType, UserListType userListType);

    public Task RemoveList(ClaimsPrincipal? claimsUser, string listName);
    public Task RemoveList(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType);
    public Task RemoveList(string userName, ContentType contentType, UserListType userListType);
    public Task RemoveList(string userName, string listName);

    public Task<ISet<UserList<T>>> GetLists(ClaimsPrincipal? claimsUser, ContentType contentType = ContentType.None);
    public Task<ISet<UserList<T>>> GetLists(string userName, ContentType contentType = ContentType.None);
}
