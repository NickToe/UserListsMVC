using Microsoft.AspNetCore.Identity;

namespace UserListsMVC.DataLayer.Repo;

public abstract class UserListBaseRepo<T> : IUserListRepo<T> where T : UserListItemBase
{
  private readonly ILogger _logger;
  protected readonly UserManager<ApplicationUser> _userManager;

  public UserListBaseRepo(ILogger logger, UserManager<ApplicationUser> userManager)
  {
    _logger = logger;
    _userManager = userManager;
  }

  public abstract Task<UserList<T>> GetWithItems(string userName, string listName);
  public abstract Task<UserList<T>> Get(string userName, string listName);

  public async Task<bool> AddItem(string userName, string listName, T item)
  {
    var userList = await GetWithItems(userName, listName);
    if (userList.Any(item.ItemId)) return false;
    userList.Add(item);
    return true;
  }

  public async Task<bool> RemoveItem(string userName, string listName, string itemId)
  {
    var userList = await GetWithItems(userName, listName);
    T? item = userList.Find(itemId);
    ArgumentNullException.ThrowIfNull(item);
    return userList.Remove(item);
  }

  public async Task<bool> UpdateItem(string userName, string listName, T item)
  {
    var userList = await GetWithItems(userName, listName);
    T? itemUpd = userList.Find(item.ItemId);
    ArgumentNullException.ThrowIfNull(itemUpd);
    userList.Update(itemUpd, item);
    return true;
  }

  public async Task<bool> UpdatePrivacy(string userName, string listName, bool isPublic)
  {
    var userList = await Get(userName, listName);
    userList.UpdatePrivacy(isPublic);
    return true;
  }

  public async Task<bool> IsListPresent(string userName, string listName) => (await GetLists(userName)).Any(list => list.Name == listName);

  public async Task<bool> AddList(string userName, string listName, ContentType contentType, UserListType userListType)
  {
    _logger.LogInformation("AddList(): Adding list '{0} {1}' with name {2} for user {3}", contentType, userListType, listName, userName);
    if (await IsListPresent(userName, listName)) return false;
    return (await GetLists(userName)).Add(new(listName, contentType, userListType));
  }

  public async Task<bool> RemoveList(string userName, string listName)
  {
    var userList = await Get(userName, listName);
    return (await GetLists(userName)).Remove(userList);
  }

  public abstract Task<ISet<UserList<T>>> GetLists(string userName);
}