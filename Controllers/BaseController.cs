using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace UserListsMVC.Controllers;

public class BaseController : Controller
{
    private readonly ILogger _logger;
    protected string UserId
    {
        get
        {
            string? userId = HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
            if (userId == default)
            {
                if (HttpContext.Request.Cookies.ContainsKey("CookieUserId"))
                {
                    userId = HttpContext.Request.Cookies["CookieUserId"];
                }
                else
                {
                    CookieOptions options = new();
                    options.Expires = DateTime.Now.AddDays(31);
                    userId = Guid.NewGuid().ToString();
                    HttpContext?.Response.Cookies.Append("CookieUserId", userId, options);
                }
            }
            return userId;
        }
    }

    public BaseController(ILogger logger)
    {
        _logger = logger;
    }
}
