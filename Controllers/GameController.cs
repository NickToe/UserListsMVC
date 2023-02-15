using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.Application.Abstractions;
using UserListsMVC.Application.DTOs;
using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;
using UserListsMVC.ViewModels;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class GameController : BaseController
{
    private readonly ILogger<GameController> _logger;
    private readonly IUserService _userService;
    private readonly IItemService<GameDTO> _gameService;
    private readonly IItemInfoService _itemInfoService;
    public GameController(ILogger<GameController> logger, IUserService userRepo, IItemService<GameDTO> gameService, IItemInfoService itemInfoService) : base(logger)
    {
        _logger = logger;
        _userService = userRepo;
        _gameService = gameService;
        _itemInfoService = itemInfoService;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search(string title, [FromServices] IUserListService<CustomListItem> customListService)
    {
        string userName = _userService.GetUserName(User);
        ContentType contentType = ContentType.Game;
        if (string.IsNullOrEmpty(title)) return View(new AddItemPageViewModel<GameDTO>(userName, contentType));
        _logger.LogInformation("Searching for {title} games...", title);
        AddItemPageViewModel<GameDTO> model = new(userName, contentType) { CustomLists = await customListService.GetLists(userName, contentType), Items = await _gameService.GetByName(title) };
        return View(model);
    }

    [HttpGet("Get/{itemId}")]
    public async Task<IActionResult> Get(string itemId)
    {
        GetItemPageViewModel<GameDTO> model = new(await _gameService.GetById(itemId), await _itemInfoService.Get(itemId, UserId));
        return View(model);
    }
}