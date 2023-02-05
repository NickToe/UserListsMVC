using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class MovieController : BaseController
{
  private readonly ILogger<MovieController> _logger;
  private readonly IUserRepo _userRepo;
  private readonly IItemService<Movie> _movieService;
  private readonly IItemInfoService _itemInfoService;
  public MovieController(ILogger<MovieController> logger, IUserRepo userRepo, IItemService<Movie> movieService, IItemInfoService itemInfoService) : base(logger)
  {
    _logger = logger;
    _userRepo = userRepo;
    _movieService = movieService;
    _itemInfoService = itemInfoService;
  }

  [HttpGet("Search")]
  public async Task<IActionResult> Search(string title, [FromServices] IUserListService<CustomListItem> customListService)
  {
    string userName = _userRepo.GetUserName(User);
    ContentType contentType = ContentType.Movie;
    if (string.IsNullOrEmpty(title)) return View(new AddItemPageViewModel<Movie>(userName, contentType));
    _logger.LogInformation("Searching for {title} movies...", title);
    AddItemPageViewModel<Movie> model = new(userName, contentType) { CustomLists = await customListService.GetLists(userName, contentType), Items = await _movieService.GetByName(title) };
    return View(model);
  }

  [HttpGet("Get/{itemId}")]
  public async Task<IActionResult> Get(string itemId)
  {
    GetItemPageViewModel<Movie> model = new(await _movieService.GetById(itemId), await _itemInfoService.Get(itemId, UserId));
    return View(model);
  }
}