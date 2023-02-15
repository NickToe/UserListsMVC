using Application.Abstractions;
using Application.DTOs;
using Domain.Entities;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class MovieController : BaseController
{
    private readonly ILogger<MovieController> _logger;
    private readonly IUserService _userService;
    private readonly IItemService<MovieDTO> _movieService;
    private readonly IItemInfoService _itemInfoService;
    public MovieController(ILogger<MovieController> logger, IUserService userRepo, IItemService<MovieDTO> movieService, IItemInfoService itemInfoService) : base(logger)
    {
        _logger = logger;
        _userService = userRepo;
        _movieService = movieService;
        _itemInfoService = itemInfoService;
    }

    [HttpGet("Search")]
    public async Task<IActionResult> Search(string title, [FromServices] IUserListService<CustomListItem> customListService)
    {
        string userName = _userService.GetUserName(User);
        ContentType contentType = ContentType.Movie;
        if (string.IsNullOrEmpty(title)) return View(new AddItemPageViewModel<MovieDTO>(userName, contentType));
        _logger.LogInformation("Searching for {title} movies...", title);
        AddItemPageViewModel<MovieDTO> model = new(userName, contentType) { CustomLists = await customListService.GetLists(userName, contentType), Items = await _movieService.GetByName(title) };
        return View(model);
    }

    [HttpGet("Get/{itemId}")]
    public async Task<IActionResult> Get(string itemId)
    {
        GetItemPageViewModel<MovieDTO> model = new(await _movieService.GetById(itemId), await _itemInfoService.Get(itemId, UserId));
        return View(model);
    }
}