using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers;

[Controller]
[Authorize]
public class FollowlistController : Controller
{
    private readonly ILogger<FollowlistController> _logger;
    private readonly IUserListService<FollowlistItem> _userListService;
    private readonly UserListType _listType = UserListType.Followlist;
    private string UserName => User?.Identity?.Name ?? throw new Exception("UserName can't be retrieved");

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
        UserListItemsViewModel<UserList<FollowlistItem>> model = new(User, await _userListService.Get(UserName, contentType, _listType));
        return View(model);
    }

    [HttpGet("{contentType}/Followlist/UpdatePrivacy")]
    public async Task<IActionResult> UpdatePrivacy(ContentType contentType, bool isPublic)
    {
        _logger.LogInformation("Setting isPublic='{isPublic}' for UserList", isPublic);
        await _userListService.UpdatePrivacy(UserName, contentType, _listType, isPublic);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Followlist/Add")]
    public async Task<IActionResult> Add(ContentType contentType, string poster, string itemId, string title)
    {
        _logger.LogInformation($"Adding item (id: '{itemId}', title: '{title}') to game followlist for user {UserName}");

        FollowlistItem followlistItem = new(itemId, title, poster);
        await _userListService.Add(UserName, contentType, _listType, followlistItem);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Followlist/Remove")]
    public async Task<IActionResult> Remove(ContentType contentType, string itemId)
    {
        _logger.LogInformation($"Removing item with id '{itemId}' from game followlist for user {UserName}");
        await _userListService.Remove(UserName, contentType, _listType, itemId);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/Followlist/Update")]
    public async Task<IActionResult> Update(ContentType contentType, FollowlistItemDTO followlistUpdateModel)
    {
        _logger.LogInformation($"UpdateInFollowlist(): {followlistUpdateModel} {UserName}");
        await _userListService.Update(UserName, contentType, _listType, followlistUpdateModel);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}