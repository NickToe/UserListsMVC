using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Claims;

namespace UserListsMVC.Controllers;

public class HomeController : Controller
{
  private readonly ILogger<HomeController> _logger;
  private readonly IHttpContextAccessor _contextAccessor;
  private string UserId
  {
    get
    {
      string? userId = HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
      if(userId == default)
      {
        if(_contextAccessor.HttpContext.Request.Cookies.ContainsKey("CookieUserId"))
        {
          userId = _contextAccessor.HttpContext.Request.Cookies["CookieUserId"];
          _logger.LogInformation("User is anonymous and CookieUserId is: {userId}", userId);
        }
        else
        {
          CookieOptions options = new();
          options.Expires = DateTime.Now.AddDays(31);
          userId = Guid.NewGuid().ToString();
          _contextAccessor?.HttpContext?.Response.Cookies.Append("CookieUserId", userId, options);
          _logger.LogInformation("User is anonymous and new CookieUserId is: {userId}", userId);
        }
      }
      else
      {
        _logger.LogInformation("User is logged in and id: {userId}", userId);
      }
      return userId;
    }
  }


  public HomeController(ILogger<HomeController> logger, IHttpContextAccessor contextAccessor)
  {
    _logger = logger;
    _contextAccessor = contextAccessor;
  }

  public IActionResult Index()
  {
    _logger.LogInformation("UserId: {userId}", UserId);
    return View();
  }

  public IActionResult Privacy()
  {
    return View();
  }

  [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
  public IActionResult Error()
  {
    var error = HttpContext.Features.Get<IExceptionHandlerFeature>();

    return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier, Description = error?.Error.Message ?? "Couldn't get error message" });
  }
}