using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;
using UserListsMVC.Infrastructure.Events;
using UserListsMVC.Infrastructure.Events.Models;

namespace UserListsMVC.Application.Abstractions;

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
