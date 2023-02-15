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
public class CustomListController : Controller
{
    private readonly ILogger<CustomListController> _logger;
    private readonly IUserListService<CustomListItem> _userListService;
    private readonly UserListType _listType = UserListType.CustomList;
    private string UserName => User?.Identity?.Name ?? throw new Exception("UserName can't be retrieved");

    public CustomListController(ILogger<CustomListController> logger, IUserListService<CustomListItem> userListService)
    {
        _logger = logger;
        _userListService = userListService;
    }

    [HttpGet("{contentType}/{userName}/CustomList/{listName}")]
    public async Task<IActionResult> GetForUser(ContentType contentType, string listName, string userName)
    {
        _logger.LogInformation("Get(): Getting '{contentType} {listType}' with name '{listName}' for user {userName}", contentType, _listType, listName, userName);
        UserListItemsViewModel<UserList<CustomListItem>> model = new(userName, await _userListService.Get(userName, listName));
        return View(model);
    }

    [HttpGet("{contentType}/CustomList/{listName}")]
    public async Task<IActionResult> Get(ContentType contentType, string listName)
    {
        _logger.LogInformation("Get(): Getting '{contentType} {listType}' with name '{listName}' for myself", contentType, _listType, listName);
        UserListItemsViewModel<UserList<CustomListItem>> model = new(User, await _userListService.Get(UserName, listName));
        return View(model);
    }

    [HttpGet("{contentType}/{listName}/UpdatePrivacy")]
    public async Task<IActionResult> UpdatePrivacy(ContentType contentType, string listName, bool isPublic)
    {
        _logger.LogInformation("Setting isPublic='{isPublic}' for UserList", isPublic);
        await _userListService.UpdatePrivacy(UserName, listName, isPublic);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/{listName}/Add")]
    public async Task<IActionResult> Add(ContentType contentType, string listName, string poster, string itemId, string title)
    {
        _logger.LogInformation($"Adding item (id: '{itemId}', title: '{title}') to game followlist for user {UserName}");

        CustomListItem customListItem = new(itemId, title, poster);
        await _userListService.Add(UserName, listName, customListItem);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/{listName}/Remove")]
    public async Task<IActionResult> Remove(ContentType contentType, string listName, string itemId)
    {
        _logger.LogInformation($"Removing item with id '{itemId}' from game followlist for user {UserName}");
        await _userListService.Remove(UserName, listName, itemId);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/{listName}/Update")]
    public async Task<IActionResult> Update(ContentType contentType, string listName, CustomListItemDTO customListItemDTO)
    {
        _logger.LogInformation($"Update(): {customListItemDTO} {UserName}");
        await _userListService.Update(UserName, listName, customListItemDTO);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/CustomList/AddList")]
    public async Task<IActionResult> AddList(ContentType contentType, string listName)
    {
        _logger.LogInformation("AddList(): Adding '{contentType} {listType}' with name {listName} for user {userName}", contentType, _listType, listName, UserName);
        await _userListService.AddList(UserName, listName, contentType, _listType);
        return Redirect(Request.Headers["Referer"].ToString());
    }

    [HttpGet("{contentType}/{listName}/RemoveList")]
    public async Task<IActionResult> RemoveList(ContentType contentType, string listName)
    {
        _logger.LogInformation("RemoveList(): Removing '{contentType} {listType}' with name {listName} for user {userName}", contentType, _listType, listName, UserName);
        await _userListService.RemoveList(UserName, listName);
        return RedirectToAction("List", new { contentType });
    }

    [HttpGet("{contentType}/CustomList/List")]
    public async Task<IActionResult> List(ContentType contentType)
    {
        UserListViewModel model = new(UserName, contentType) { CustomLists = await _userListService.GetLists(UserName, contentType) };
        return View(model);
    }
}