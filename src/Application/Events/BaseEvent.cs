using Application.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Events;

public abstract class BaseEvent
{
    protected readonly INotificationService _notificationService;
    public BaseEvent(IServiceProvider serviceProvider)
    {
        _notificationService = serviceProvider.CreateScope().ServiceProvider.GetRequiredService<INotificationService>();
    }

    public string Id { get; set; } = new Guid().ToString();
    public DateTime EventAdded { get; set; } = DateTime.Now;

    public abstract Task Process();
}
