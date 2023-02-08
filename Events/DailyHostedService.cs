using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Events;

public class DailyHostedService : BackgroundService
{
  private readonly ILogger<DailyHostedService> _logger;
  private readonly IServiceProvider _serviceProvider;
  public DailyHostedService(ILogger<DailyHostedService> logger, IServiceProvider serviceProvider)
  {
    _logger = logger;
    _serviceProvider = serviceProvider;
  }

  protected async override Task ExecuteAsync(CancellationToken stoppingToken)
  {
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        using (var scope = _serviceProvider.CreateScope())
        {
          var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
          await notificationService.AddPlannedNotifs();
          var emailNotificationService = scope.ServiceProvider.GetRequiredService<IEmailNotificationService>();
          await emailNotificationService.SendEmailAsync();
        }
      }
      catch (Exception ex)
      {
        _logger.LogError("Exception while handling daily task: {exception}", ex.Message);
      }
      TimeSpan timeSpan = new TimeOnly(0, 0, 0) - TimeOnly.FromDateTime(DateTime.Now);
      await Task.Delay(timeSpan);
    }
  }
}
