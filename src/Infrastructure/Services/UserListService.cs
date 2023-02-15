using Application.Abstractions;
using Application.DTOs;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using Infrastructure.Services.UserListHelpers;
using Microsoft.Extensions.Logging;

namespace Infrastructure.Services;

public class UserListService<T> : IUserListService<T> where T : UserListItemBase
{
    private readonly ILogger<UserListService<T>> _logger;
    private readonly IUserService _userService;
    private readonly IUserListHelper<T> _userListHelper;
    public UserListService(ILogger<UserListService<T>> logger, IUserService userService, IUserListHelper<T> userListHelper)
    {
        _logger = logger;
        _userService = userService;
        _userListHelper = userListHelper;
    }

    public async Task<UserList<T>> Get(string userName, ContentType contentType, UserListType userListType) =>
      await Get(userName, DefaultUserListNames.GetName(contentType, userListType));

    public async Task<UserList<T>> Get(string userName, string listName)
    {
        return await _userListHelper.GetWithItems(userName, listName);
    }

    public async Task Add(string userName, ContentType contentType, UserListType userListType, T item) =>
      await Add(userName, DefaultUserListNames.GetName(contentType, userListType), item);

    public async Task Add(string userName, string listName, T item)
    {
        if (await _userListHelper.AddItem(userName, listName, item)) await _userService.Update(userName);
    }

    public async Task Remove(string userName, ContentType contentType, UserListType userListType, string itemId)
    => await Remove(userName, DefaultUserListNames.GetName(contentType, userListType), itemId);

    public async Task Remove(string userName, string listName, string itemId)
    {
        if (await _userListHelper.RemoveItem(userName, listName, itemId)) await _userService.Update(userName);
    }

    public async Task Update(string userName, ContentType contentType, UserListType userListType, ListItemBaseDTO item) =>
      await Update(userName, DefaultUserListNames.GetName(contentType, userListType), item);

    public async Task Update(string userName, string listName, ListItemBaseDTO item)
    {
        if (await _userListHelper.UpdateItem(userName, listName, item)) await _userService.Update(userName);
    }

    public async Task UpdatePrivacy(string userName, ContentType contentType, UserListType userListType, bool isPublic) =>
      await UpdatePrivacy(userName, DefaultUserListNames.GetName(contentType, userListType), isPublic);

    public async Task UpdatePrivacy(string userName, string listName, bool isPublic)
    {
        if (await _userListHelper.UpdatePrivacy(userName, listName, isPublic)) await _userService.Update(userName);
    }

    public async Task AddList(string userName, ContentType contentType, UserListType userListType) =>
      await AddList(userName, DefaultUserListNames.GetName(contentType, userListType), contentType, userListType);

    public async Task AddList(string userName, string listName, ContentType contentType, UserListType userListType)
    {
        if (await _userListHelper.AddList(userName, listName, contentType, userListType)) await _userService.Update(userName);
    }

    public async Task RemoveList(string userName, ContentType contentType, UserListType userListType) =>
      await RemoveList(userName, DefaultUserListNames.GetName(contentType, userListType));

    public async Task RemoveList(string userName, string listName)
    {
        if (await _userListHelper.RemoveList(userName, listName)) await _userService.Update(userName);
    }

    public async Task<ISet<UserList<T>>> GetLists(string userName, ContentType contentType = ContentType.None)
    {
        if (string.IsNullOrEmpty(userName)) return new HashSet<UserList<T>>();
        var userLists = await _userListHelper.GetLists(userName);
        return contentType == ContentType.None ? userLists : userLists.Where(list => list.ContentType == contentType).ToHashSet();
    }
}