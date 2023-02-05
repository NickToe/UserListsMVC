using UserListsMVC.Controllers;
using UserListsMVC.DataLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

// IHttpContextAccessor contextAccessor
// string contentType = _contextAccessor.HttpContext.Request.RouteValues["contentType"].ToString();

[Controller]
[Authorize]
public class FollowlistController : Controller
{
  private readonly ILogger<FollowlistController> _logger;
  private readonly IUserListService<FollowlistItem> _userListService;
  private readonly UserListType _listType = UserListType.Followlist;

  public FollowlistController(ILogger<FollowlistController> logger, IUserListService<FollowlistItem> userListService)
  {
    _logger = logger;
    _userListService = userListService;
  }

  [HttpGet("{contentType}/{userName}/Followlist")]
  public async Task<IActionResult> GetForUser(ContentType contentType, string userName)
  {
    UserListItemsViewModel<UserList<FollowlistItem>> model = new(userName, await _userListService.Get(userName, contentType, _listType));
    return View(model);
  }

  [HttpGet("{contentType}/Followlist")]
  public async Task<IActionResult> Get(ContentType contentType)
  {
    UserListItemsViewModel<UserList<FollowlistItem>> model = new(User, await _userListService.Get(User, contentType, _listType));
    return View(model);
  }

  [HttpGet("{contentType}/Followlist/UpdatePrivacy")]
  public async Task<IActionResult> UpdatePrivacy(ContentType contentType, bool isPublic)
  {
    _logger.LogInformation("Setting isPublic='{isPublic}' for UserList", isPublic);
    await _userListService.UpdatePrivacy(User, contentType, _listType, isPublic);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/Followlist/Add")]
  public async Task<IActionResult> Add(ContentType contentType, string poster, string itemId, string title)
  {
    _logger.LogInformation($"Adding item (id: '{itemId}', title: '{title}') to game followlist for user {User?.Identity?.Name}");
    
    FollowlistItem followlistItem = new(itemId, title, poster);
    await _userListService.Add(User, contentType, _listType, followlistItem);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/Followlist/Remove")]
  public async Task<IActionResult> Remove(ContentType contentType, string itemId)
  {
    _logger.LogInformation($"Removing item with id '{itemId}' from game followlist for user {User?.Identity?.Name}");
    await _userListService.Remove(User, contentType, _listType, itemId);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("{contentType}/Followlist/Update")]
  public async Task<IActionResult> Update(ContentType contentType, FollowlistUpdateModel followlistUpdateModel)
  {
    _logger.LogInformation($"UpdateInFollowlist(): {followlistUpdateModel} {User?.Identity?.Name}");

    FollowlistItem item = new(followlistUpdateModel);
    await _userListService.Update(User, contentType, _listType, item);
    return Redirect(Request.Headers["Referer"].ToString());
  }
}