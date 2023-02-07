using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class NotificationController : Controller
{
  private readonly ILogger<NotificationController> _logger;
  private readonly INotificationService _notificationService;
  private string UserId => HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? String.Empty;

  public NotificationController(ILogger<NotificationController> logger, INotificationService notificationService)
  {
    _logger = logger;
    _notificationService = notificationService;
  }

  [HttpGet("Remove/{notifId?}")]
  public async Task<IActionResult> Remove(NotifType notifType, int? notifId)
  {
    await _notificationService.Remove(UserId, notifType, notifId);
    return Redirect(Request.Headers["Referer"].ToString());
  }
}