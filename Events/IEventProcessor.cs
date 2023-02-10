namespace UserListsMVC.Events;

public interface IEventProcessor
{
    public void AddEvent(BaseEvent baseEvent);
    public Task ProcessEvents();
}
