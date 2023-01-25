using Microsoft.AspNetCore.Mvc;
using UserListsMVC.DataLayer.Repo;
using Microsoft.AspNetCore.Authorization;
using UserListsMVC.ServiceLayer;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class GameController : Controller
{
  private readonly ILogger<GameController> _logger;
  private readonly IUserRepo _userRepo;
  private readonly IItemService<Game> _gameService;
  public GameController(ILogger<GameController> logger, IUserRepo userRepo, IItemService<Game> gameService)
  {
    _logger = logger;
    _userRepo = userRepo;
    _gameService = gameService;
  }

  /*if (items.Any())
{
  IEnumerable<string> gameIds = items.Select(item => item.ItemId.ToString());
  games = await _gameService.GetGamesByIds(gameIds);
}*/
  //Caching:   [ResponseCache(CacheProfileName = "Caching")]

  public IActionResult Index()
  {
    return View();
  }

  [HttpGet("Search")]
  public async Task<IActionResult> Search(string title, [FromServices] IUserListService<CustomListItem> customListService)
  {
    string userName = _userRepo.GetUserName(User);
    if (string.IsNullOrEmpty(title)) return View(new AddItemPageViewModel<Game>(userName, ContentType.Game));
    _logger.LogInformation("Searching for {title} games...", title);
    AddItemPageViewModel<Game> model = new(userName, ContentType.Game) { CustomLists = await customListService.GetLists(userName, ContentType.Game), Items = await _gameService.GetByName(title) };
    return View(model);
  }
}