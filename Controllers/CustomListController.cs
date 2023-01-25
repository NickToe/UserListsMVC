using UserListsMVC.DataLayer.ViewModels;
using UserListsMVC.ServiceLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UserListsMVC.Controllers;

[Controller]
[Authorize]
public class CustomListController : Controller
{
  private readonly ILogger<CustomListController> _logger;
  private readonly IUserListService<CustomListItem> _userListService;
  private readonly UserListType _listType = UserListType.CustomList;

  public CustomListController(ILogger<CustomListController> logger, IUserListService<CustomListItem> userListService)
  {
    _logger = logger;
    _userListService = userListService;
  }

  [HttpGet("{contentType}/{userName}/{listName}")]
  public async Task<IActionResult> GetForUser(ContentType contentType, string listName, string userName)
  {
    _logger.LogInformation("Get(): Getting '{contentType} {listType}' with name '{listName}' for user {userName}", contentType, _listType, listName, userName);
    UserListItemsViewModel<UserList<CustomListItem>> model = new(userName, await _userListService.Get(userName, listName));
    return View(model);
  }

  [HttpGet("{contentType}/{listName}")]
  public async Task<IActionResult> Get(ContentType contentType, string listName)
  {
    _logger.LogInformation("Get(): Getting '{contentType} {listType}' with name '{listName}' for myself", contentType, _listType, listName);
    UserListItemsViewModel<UserList<CustomListItem>> model = new(User, await _userListService.Get(User, listName));
    return View(model);
  }

  [HttpGet("{contentType}/{listName}/UpdatePrivacy")]
  public async Task<IActionResult> UpdatePrivacy(ContentType contentType, string listName, bool isPublic)
  {
    _logger.LogInformation("Setting isPublic='{isPublic}' for UserList", isPublic);
    await _userListService.UpdatePrivacy(User, listName, isPublic);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/{listName}/Add")]
  public async Task<IActionResult> Add(ContentType contentType, string listName, string poster, string itemId, string title)
  {
    Console.WriteLine($"Adding item (id: '{itemId}', title: '{title}') to game followlist for user {User?.Identity?.Name}");

    CustomListItem customListItem = new(itemId, title, poster);
    await _userListService.Add(User, listName, customListItem);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/{listName}/Remove")]
  public async Task<IActionResult> Remove(ContentType contentType, string listName, string itemId)
  {
    Console.WriteLine($"Removing item with id '{itemId}' from game followlist for user {User?.Identity?.Name}");
    await _userListService.Remove(User, listName, itemId);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/{listName}/Update")]
  public async Task<IActionResult> Update(ContentType contentType, string listName, CustomListUpdateModel customListUpdateModel)
  {
    Console.WriteLine($"Update(): {customListUpdateModel} {User?.Identity?.Name}");

    CustomListItem item = new(customListUpdateModel);
    await _userListService.Update(User, listName, item);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/CustomList/AddList")]
  public async Task<IActionResult> AddList(ContentType contentType, string listName)
  {
    _logger.LogInformation("AddList(): Adding '{contentType} {listType}' with name {listName} for user {userName}", contentType, _listType, listName, User?.Identity?.Name);
    await _userListService.AddList(User, listName, contentType, _listType);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/{listName}/RemoveList")]
  public async Task<IActionResult> RemoveList(ContentType contentType, string listName)
  {
    _logger.LogInformation("RemoveList(): Removing '{contentType} {listType}' with name {listName} for user {userName}", contentType, _listType, listName, User?.Identity?.Name);
    await _userListService.RemoveList(User, listName);
    return RedirectToAction("List", new {contentType = contentType});
  }

  [HttpGet("{contentType}/CustomList/List")]
  public async Task<IActionResult> List(ContentType contentType)
  {
    UserListViewModel model = new(User?.Identity?.Name, contentType) { CustomLists = await _userListService.GetLists(User, contentType) };
    return View(model);
  }
}