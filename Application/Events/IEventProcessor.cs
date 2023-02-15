using UserListsMVC.Infrastructure.Events;

namespace UserListsMVC.Application.Events;

public interface IEventProcessor
{
    public void AddEvent(BaseEvent baseEvent);
    public Task ProcessEvents();
}
