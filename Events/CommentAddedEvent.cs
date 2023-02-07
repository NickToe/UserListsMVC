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
    Console.WriteLine("Sending notification of type {0}", typeof(CommentAddedEvent));
  }
}