using Application.DTOs;
using Domain.Entities;
using Domain.Enums;

namespace Application.Abstractions;

public interface IUserListService<T> where T : UserListItemBase
{
    public Task<UserList<T>> Get(string userName, ContentType contentType, UserListType userListType);
    public Task<UserList<T>> Get(string userName, string listName);

    public Task Add(string userName, ContentType contentType, UserListType userListType, T item);
    public Task Add(string userName, string listName, T item);

    public Task Remove(string userName, ContentType contentType, UserListType userListType, string itemId);
    public Task Remove(string userName, string listName, string itemId);

    public Task Update(string userName, ContentType contentType, UserListType userListType, ListItemBaseDTO item);
    public Task Update(string userName, string listName, ListItemBaseDTO item);

    public Task UpdatePrivacy(string userName, ContentType contentType, UserListType userListType, bool isPublic);
    public Task UpdatePrivacy(string userName, string listName, bool isPublic);

    public Task AddList(string userName, ContentType contentType, UserListType userListType);
    public Task AddList(string userName, string listName, ContentType contentType, UserListType userListType);

    public Task RemoveList(string userName, ContentType contentType, UserListType userListType);
    public Task RemoveList(string userName, string listName);

    public Task<ISet<UserList<T>>> GetLists(string userName, ContentType contentType = ContentType.None);
}
