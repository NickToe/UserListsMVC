using System.Security.Claims;
using Application.Abstractions;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class NotificationController : Controller
{
    private readonly ILogger<NotificationController> _logger;
    private readonly INotificationService _notificationService;
    private string UserId => HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? string.Empty;

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