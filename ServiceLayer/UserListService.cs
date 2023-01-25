using System.Security.Claims;
using UserListsMVC.DataLayer.Repo;

namespace UserListsMVC.ServiceLayer;

public class UserListService<T> : IUserListService<T> where T : UserListItemBase
{
  private readonly ILogger<UserListService<T>> _logger;
  private readonly IUserRepo _userRepo;
  private readonly IUserListRepo<T> _userListRepo;
  public UserListService(ILogger<UserListService<T>> logger, IUserRepo userRepo, IUserListRepo<T> userListRepo)
  {
    _logger = logger;
    _userRepo = userRepo;
    _userListRepo = userListRepo;
  }

  //Get block
  public async Task<UserList<T>> Get(ClaimsPrincipal? claimsUser, string listName) =>
    await Get(_userRepo.GetUserName(claimsUser), listName);
  public async Task<UserList<T>> Get(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType) =>
    await Get(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType));
  public async Task<UserList<T>> Get(string userName, ContentType contentType, UserListType userListType) =>
    await Get(userName, PredefinedListNames.GetName(contentType, userListType));

  public async Task<UserList<T>> Get(string userName, string listName)
  {
    return await _userListRepo.GetWithItems(userName, listName);
  }

  // Add block
  public async Task Add(ClaimsPrincipal? claimsUser, string listName, T item) =>
    await Add(_userRepo.GetUserName(claimsUser), listName, item);
  public async Task Add(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, T item) =>
    await Add(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType), item);
  public async Task Add(string userName, ContentType contentType, UserListType userListType, T item) =>
    await Add(userName, PredefinedListNames.GetName(contentType, userListType), item);

  public async Task Add(string userName, string listName, T item)
  {
    if (await _userListRepo.AddItem(userName, listName, item)) await _userRepo.Update(userName);
  }

  // Remove block
  public async Task Remove(ClaimsPrincipal? claimsUser, string listName, string itemId)
    => await Remove(_userRepo.GetUserName(claimsUser), listName, itemId);
  public async Task Remove(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, string itemId)
  => await Remove(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType), itemId);
  public async Task Remove(string userName, ContentType contentType, UserListType userListType, string itemId)
  => await Remove(userName, PredefinedListNames.GetName(contentType, userListType), itemId);

  public async Task Remove(string userName, string listName, string itemId)
  {
    if (await _userListRepo.RemoveItem(userName, listName, itemId)) await _userRepo.Update(userName);
  }

  // Update block
  public async Task Update(ClaimsPrincipal? claimsUser, string listName, T item) =>
    await Update(_userRepo.GetUserName(claimsUser), listName, item);
  public async Task Update(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, T item) =>
    await Update(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType), item);
  public async Task Update(string userName, ContentType contentType, UserListType userListType, T item) =>
    await Update(userName, PredefinedListNames.GetName(contentType, userListType), item);

  public async Task Update(string userName, string listName, T item)
  {
    if (await _userListRepo.UpdateItem(userName, listName, item)) await _userRepo.Update(userName);
  }

  // UpdatePrivacy block
  public async Task UpdatePrivacy(ClaimsPrincipal? claimsUser, string listName, bool isPublic) =>
    await UpdatePrivacy(_userRepo.GetUserName(claimsUser), listName, isPublic);
  public async Task UpdatePrivacy(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType, bool isPublic) =>
    await UpdatePrivacy(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType), isPublic);
  public async Task UpdatePrivacy(string userName, ContentType contentType, UserListType userListType, bool isPublic) =>
    await UpdatePrivacy(userName, PredefinedListNames.GetName(contentType, userListType), isPublic);

  public async Task UpdatePrivacy(string userName, string listName, bool isPublic)
  {
    if (await _userListRepo.UpdatePrivacy(userName, listName, isPublic)) await _userRepo.Update(userName);
  }

  // AddList block
  public async Task AddList(ClaimsPrincipal? claimsUser, string listName, ContentType contentType, UserListType userListType) =>
    await AddList(_userRepo.GetUserName(claimsUser), listName, contentType, userListType);
  public async Task AddList(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType) =>
    await AddList(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType), contentType, userListType);
  public async Task AddList(string userName, ContentType contentType, UserListType userListType) =>
    await AddList(userName, PredefinedListNames.GetName(contentType, userListType), contentType, userListType);

  public async Task AddList(string userName, string listName, ContentType contentType, UserListType userListType)
  {
    if (await _userListRepo.AddList(userName, listName, contentType, userListType)) await _userRepo.Update(userName);
  }

  // RemoveList block
  public async Task RemoveList(ClaimsPrincipal? claimsUser, string listName) =>
    await RemoveList(_userRepo.GetUserName(claimsUser), listName);
  public async Task RemoveList(ClaimsPrincipal? claimsUser, ContentType contentType, UserListType userListType) =>
    await RemoveList(_userRepo.GetUserName(claimsUser), PredefinedListNames.GetName(contentType, userListType));
  public async Task RemoveList(string userName, ContentType contentType, UserListType userListType) =>
    await RemoveList(userName, PredefinedListNames.GetName(contentType, userListType));

  public async Task RemoveList(string userName, string listName)
  {
    if(await _userListRepo.RemoveList(userName, listName)) await _userRepo.Update(userName);
  }

  // GetLists block
  public async Task<ISet<UserList<T>>> GetLists(ClaimsPrincipal? claimsUser, ContentType contentType = ContentType.None) =>
    await GetLists(_userRepo.GetUserName(claimsUser), contentType);

  public async Task<ISet<UserList<T>>> GetLists(string userName, ContentType contentType = ContentType.None)
  {
    var userLists = await _userListRepo.GetLists(userName);
    return contentType == ContentType.None ? userLists : userLists.Where(list => list.ContentType == contentType).ToHashSet();
  }
}