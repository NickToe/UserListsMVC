using Application.Abstractions;
using Domain;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MimeKit;
using MimeKit.Text;

namespace Infrastructure.Services;

public class EmailNotificationService : IEmailNotificationService
{
    private readonly ILogger<EmailNotificationService> _logger;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    public EmailNotificationService(ILogger<EmailNotificationService> logger, UserManager<ApplicationUser> userManager, IConfiguration configuration)
    {
        _logger = logger;
        _userManager = userManager;
        _configuration = configuration;
    }

    public async Task SendEmailAsync()
    {
        var usersWithNotifsCount = await _userManager.Users.Select(user => new { user.UserName, user.Email, PlannedCount = user.PlannedDateNotifs.Count, RepliedCount = user.RepliedNotifs.Count, FollowedCount = user.FollowedNotifs.Count }).ToListAsync();

        if (!usersWithNotifsCount.Any()) return;

        SmtpClient smtpClient = new();
        await smtpClient.ConnectAsync(_configuration.GetValue<string>("EmailService:Host"), _configuration.GetValue<int>("EmailService:Port"), false);
        await smtpClient.AuthenticateAsync(_configuration.GetValue<string>("EmailService:UserName"), _configuration.GetValue<string>("EmailService:Password"));

        foreach (var user in usersWithNotifsCount)
        {
            if (user.PlannedCount == 0 && user.RepliedCount == 0 && user.FollowedCount == 0) continue;

            MimeMessage message = new();
            message.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailService:SupportEmail")));
            message.To.Add(MailboxAddress.Parse(user.Email));
            message.Subject = "UserListsMVC: You have unread notifications!";

            string htmlMessage = $"<p> Hello dear {user.UserName} </p> <br> <p> There are unread notifications that need your attention &#128522 </p> <p> Planned: {user.PlannedCount} </p> <p> Replies: {user.RepliedCount} </p> <p> Followed: {user.FollowedCount} </p>";
            message.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

            _logger.LogInformation($"Sending message to email {user.Email} for user {user.UserName}: planned = {user.PlannedCount}, replied = {user.RepliedCount}, followed = {user.FollowedCount}");
            await smtpClient.SendAsync(message);
        }
        await smtpClient.DisconnectAsync(true);
    }
}
