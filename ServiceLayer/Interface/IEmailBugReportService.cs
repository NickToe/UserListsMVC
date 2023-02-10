namespace UserListsMVC.ServiceLayer.Interface;

public interface IEmailBugReportService
{
    public Task SendAsync(string email, string subject, string description);
}
