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
    TimeSpan timeSpan = new TimeOnly(0, 0, 0) - TimeOnly.FromDateTime(DateTime.Now);
    while (!stoppingToken.IsCancellationRequested)
    {
      try
      {
        using (var scope = _serviceProvider.CreateScope())
        {
          var notificationService = scope.ServiceProvider.GetRequiredService<INotificationService>();
          CustomStopwatch.Start();
          await notificationService.AddPlannedNotifs();
          CustomStopwatch.Stop(1);
        }
      }
      catch (Exception ex)
      {
        _logger.LogInformation("Exception while handling daily task: {exception}", ex.Message);
      }
      await Task.Delay(timeSpan);
      timeSpan = new TimeSpan(24, 0, 0);
    }
  }
}
