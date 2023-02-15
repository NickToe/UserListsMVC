namespace Application.Abstractions;

public interface IEmailNotificationService
{
    public Task SendEmailAsync();
}
