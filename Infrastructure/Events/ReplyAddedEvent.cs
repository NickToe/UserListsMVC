using UserListsMVC.Infrastructure.Events.Models;

namespace UserListsMVC.Infrastructure.Events;

public class ReplyAddedEvent : BaseEvent
{
    public ReplyAddedEvent(IServiceProvider serviceProvider, CommentNotifData notifData) : base(serviceProvider)
    {
        NotifData = notifData;
    }

    public CommentNotifData NotifData { get; set; }

    public async override Task Process()
    {
        await _notificationService.AddRepliedNotifs(NotifData);
        await _notificationService.AddFollowedNotifs(NotifData);
    }
}