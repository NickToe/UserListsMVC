using System.Reflection;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity.UI.Services;
using MimeKit;
using MimeKit.Text;

namespace UserListsMVC.ServiceLayer.Implementation;

public class EmailSender : IEmailSender
{
  private readonly IConfiguration _configuration;
  private readonly ILogger<EmailSender> _logger;

  public EmailSender(IConfiguration configuration, ILogger<EmailSender> logger)
  {
    _configuration = configuration;
    _logger = logger;
  }

  public async Task SendEmailAsync(string email, string subject, string htmlMessage)
  {
    MimeMessage message = new();
    message.From.Add(MailboxAddress.Parse(_configuration.GetValue<string>("EmailService:SupportEmail")));
    message.To.Add(MailboxAddress.Parse(email));
    message.Subject = $"UserListsMVC: {subject}";
    message.Body = new TextPart(TextFormat.Html) { Text = htmlMessage };

    using (var client = new SmtpClient())
    {
      await client.ConnectAsync(_configuration.GetValue<string>("EmailService:Host"), _configuration.GetValue<int>("EmailService:Port"), false);
      await client.AuthenticateAsync(_configuration.GetValue<string>("EmailService:UserName"), _configuration.GetValue<string>("EmailService:Password"));
      await client.SendAsync(message);
      await client.DisconnectAsync(true);
    }
  }
}