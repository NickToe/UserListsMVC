using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebUI.ViewModels;

namespace WebUI.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
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
        _logger.LogError("Exception generated for user({user}): endpoint({endpoint}), path({path}), routeValues({routeValues})", HttpContext?.User?.Identity?.Name, error?.Endpoint, error?.Path, error?.RouteValues);
        _logger.LogError("Exception: {type}: {exception}", error?.Error.GetType(), error?.Error.Message);
        _logger.LogError("Stacktrace: \n{stacktrace}", error?.Error.StackTrace?.Trim());

        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext?.TraceIdentifier });
    }
}