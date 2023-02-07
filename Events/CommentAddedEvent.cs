namespace UserListsMVC.Events;

public class CommentAddedEvent : BaseEvent
{
  public CommentAddedEvent(IServiceProvider serviceProvider, CommentNotifData notifData) : base(serviceProvider)
  {
    NotifData = notifData;
  }

  public CommentNotifData NotifData { get; set; }
  public async override Task Process()
  {
    await _notificationService.AddFollowedNotifs(NotifData);
  }
}