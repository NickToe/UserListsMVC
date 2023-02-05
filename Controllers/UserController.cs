using System.Diagnostics;
using UserListsMVC.Controllers;
using UserListsMVC.DataLayer;
using UserListsMVC.DataLayer.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class UserController : Controller
{
  private readonly ILogger<UserController> _logger;
  private readonly IUserRepo _userRepo;

  public UserController(ILogger<UserController> logger, IUserRepo userRepo)
  {
    _logger = logger;
    _userRepo = userRepo;
  }

  [HttpGet("Search")]
  public async Task<IActionResult> Search(string userName)
  {
    IEnumerable<ApplicationUser> users = Enumerable.Empty<ApplicationUser>();
    if(!String.IsNullOrEmpty(userName))
    {
      users = await _userRepo.GetAllByUserName(userName);
    }
    return View(users);
  }

  [HttpGet("List")]
  public async Task<IActionResult> List()
  {
    return View(await _userRepo.GetAll());
  }

  [HttpGet("Profile/{userName}")]
  public async Task<IActionResult> Profile([FromServices] IUserListService<FollowlistItem> followlistService, [FromServices] IUserListService<WishlistItem> wishlistService, [FromServices] IUserListService<CustomListItem> customListService, string userName)
  {
    UserListViewModel model = new(userName) { Followlists = await followlistService.GetLists(userName), Wishlists = await wishlistService.GetLists(userName), CustomLists = await customListService.GetLists(userName) };
    return View(model);
  }
}
