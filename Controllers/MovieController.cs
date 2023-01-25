using Microsoft.AspNetCore.Mvc;
using UserListsMVC.ServiceLayer;
using UserListsMVC.DataLayer.Repo;
using UserListsMVC.DataLayer.Models;
using Microsoft.AspNetCore.Authorization;
using UserListsMVC.DataLayer.Entities;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class MovieController : Controller
{
  private readonly ILogger<MovieController> _logger;
  private readonly IUserRepo _userRepo;
  private readonly IItemService<Movie> _movieService;
  public MovieController(ILogger<MovieController> logger, IUserRepo userRepo, IItemService<Movie> movieService)
  {
    _logger = logger;
    _userRepo = userRepo;
    _movieService = movieService;
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
}