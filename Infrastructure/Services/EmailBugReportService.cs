using MailKit.Net.Smtp;
using MimeKit;
using MimeKit.Text;
using UserListsMVC.Application.Abstractions;

namespace UserListsMVC.Infrastructure.Services;

public class EmailBugReportService : IEmailBugReportService
{
    private readonly ILogger<EmailBugReportService> _logger;
    private readonly IConfiguration _configuration;

    public EmailBugReportService(ILogger<EmailBugReportService> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public async Task SendAsync(string email, string subject, string description)
    {
        MimeMessage message = new();
        message.From.Add(MailboxAddress.Parse(email));
        message.To.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailService:SupportEmail")));
        message.Subject = $"Bug: {subject}";
        message.Body = new TextPart(TextFormat.Text) { Text = description };

        SmtpClient smtpClient = new();
        await smtpClient.ConnectAsync(_configuration.GetValue<string>("EmailService:Host"), _configuration.GetValue<int>("EmailService:Port"), false);
        await smtpClient.AuthenticateAsync(_configuration.GetValue<string>("EmailService:UserName"), _configuration.GetValue<string>("EmailService:Password"));
        _logger.LogInformation("Sending bug report from {email} with subject: {subject}", email, subject);
        await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }
}
