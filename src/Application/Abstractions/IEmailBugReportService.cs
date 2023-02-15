namespace Application.Abstractions;

public interface IEmailBugReportService
{
    public Task SendAsync(string email, string subject, string description);
}
