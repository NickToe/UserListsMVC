using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class GameController : BaseController
{
    private readonly ILogger<GameController> _logger;
    private readonly IUserRepo _userRepo;
    private readonly IItemService<Game> _gameService;
    private readonly IItemInfoService _itemInfoService;
    public GameController(ILogger<GameController> logger, IUserRepo userRepo, IItemService<Game> gameService, IItemInfoService itemInfoService) : base(logger)
    {
        _logger = logger;
        _userRepo = userRepo;
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
        string userName = _userRepo.GetUserName(User);
        ContentType contentType = ContentType.Game;
        if (string.IsNullOrEmpty(title)) return View(new AddItemPageViewModel<Game>(userName, contentType));
        _logger.LogInformation("Searching for {title} games...", title);
        AddItemPageViewModel<Game> model = new(userName, contentType) { CustomLists = await customListService.GetLists(userName, contentType), Items = await _gameService.GetByName(title) };
        return View(model);
    }

    [HttpGet("Get/{itemId}")]
    public async Task<IActionResult> Get(string itemId)
    {
        GetItemPageViewModel<Game> model = new(await _gameService.GetById(itemId), await _itemInfoService.Get(itemId, UserId));
        return View(model);
    }
}