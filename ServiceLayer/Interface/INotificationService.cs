using UserListsMVC.Events;

namespace UserListsMVC.ServiceLayer.Interface;

public interface INotificationService
{
  public Task<IEnumerable<PlannedDateNotif>> GetPlannedNotifs(string userId);
  public Task AddPlannedNotifs();
  public Task Remove(string userId, NotifType notifType, int? notifId);
  public Task<IEnumerable<RepliedNotif>> GetRepliedNotifs(string userId);
  public Task AddRepliedNotifs(CommentNotifData notifData);
  public Task<IEnumerable<FollowedNotif>> GetFollowedNotifs(string userId);
  public Task AddFollowedNotifs(CommentNotifData notifData);
}
