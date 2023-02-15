using System.Security.Claims;
using Application.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebUI.Controllers;

[Controller]
[Route("[controller]")]
[AllowAnonymous]
public class BugReportController : Controller
{
    private readonly ILogger<BugReportController> _logger;
    private readonly IEmailBugReportService _emailBugReportService;

    public BugReportController(ILogger<BugReportController> logger, IEmailBugReportService emailBugReportService)
    {
        _logger = logger;
        _emailBugReportService = emailBugReportService;
    }

    [HttpPost("Send")]
    public async Task<IActionResult> Send(string? email, string subject, string description)
    {
        string emailFrom = email ?? HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? string.Empty;
        await _emailBugReportService.SendAsync(emailFrom, subject, description);
        return Redirect(Request.Headers["Referer"].ToString());
    }
}
