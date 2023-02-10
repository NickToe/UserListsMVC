using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Authorize]
public class WishlistController : Controller
{
    private readonly ILogger<WishlistController> _logger;
    private readonly IUserListService<WishlistItem> _userListService;
    private readonly UserListType _listType = UserListType.Wishlist;
    private string UserName => User?.Identity?.Name ?? throw new Exception("UserName can't be retrieved");

    public WishlistController(ILogger<WishlistController> logger, IUserListService<WishlistItem> userListService)
    {
        _logger = logger;
        _userListService = userListService;
    }

    [HttpGet("{contentType}/{userName}/Wishlist")]
    public async Task<IActionResult> GetForUser(ContentType contentType, string userName)
    {
        UserListItemsViewModel<UserList<WishlistItem>> model = new(userName, await _userListService.Get(userName, contentType, _listType));
        return View(model);
    }

    [HttpGet("{contentType}/Wishlist")]
    public async Task<IActionResult> Get(ContentType contentType)
    {
        UserListItemsViewModel<UserList<WishlistItem>> model = new(User, await _userListService.Get(UserName, contentType, _listType));
        ViewBag.ContentType = contentType;
        return View(model);
    }

    [HttpGet("{contentType}/Wishlist/UpdatePrivacy")]
    public async Task<IActionResult> UpdatePrivacy(ContentType contentType, bool isPublic)
    {
        _logger.LogInformation("Setting isPublic='{isPublic}' for UserList", isPublic);
        await _userListService.UpdatePrivacy(UserName, contentType, _listType, isPublic);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Wishlist/Add")]
    public async Task<IActionResult> Add(ContentType contentType, string poster, string itemId, string title)
    {
        _logger.LogInformation($"Adding item with id '{itemId}' to game wishlist for user {UserName}");
        WishlistItem followlistItem = new(itemId, title, poster);
        await _userListService.Add(UserName, contentType, _listType, followlistItem);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Wishlist/Remove")]
    public async Task<IActionResult> Remove(ContentType contentType, string itemId)
    {
        _logger.LogInformation($"Removing item with id '{itemId}' from game wishlist for user {UserName}");
        await _userListService.Remove(UserName, contentType, _listType, itemId);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Wishlist/Update")]
    public async Task<IActionResult> Update(ContentType contentType, WishlistUpdateModel wishlistUpdateModel)
    {
        _logger.LogInformation("UpdateInWishlist: wishlistupdatemodel: {wishlistupdatemodel}", wishlistUpdateModel);
        WishlistItem item = new(wishlistUpdateModel);
        await _userListService.Update(UserName, contentType, _listType, item);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}