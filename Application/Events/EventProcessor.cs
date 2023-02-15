using UserListsMVC.Infrastructure.Events;

namespace UserListsMVC.Application.Events;

public class EventProcessor : IEventProcessor
{
    private ICollection<BaseEvent> _events { get; set; } = new List<BaseEvent>();

    public void AddEvent(BaseEvent baseEvent)
    {
        _events.Add(baseEvent);
    }

    public async Task ProcessEvents()
    {
        foreach (BaseEvent baseEvent in _events)
        {
            await baseEvent.Process();
        }
        _events.Clear();
    }
}