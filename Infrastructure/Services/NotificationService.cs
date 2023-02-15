using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application.Abstractions;
using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;
using UserListsMVC.Infrastructure.Events;
using UserListsMVC.Infrastructure.Events.Models;
using UserListsMVC.Infrastructure.Persistence;

namespace UserListsMVC.Infrastructure.Services;

public class NotificationService : INotificationService
{
    private readonly ILogger<NotificationService> _logger;
    private readonly ApplicationDbContext _context;

    public NotificationService(ILogger<NotificationService> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<IEnumerable<PlannedDateNotif>> GetPlannedNotifs(string userId)
    {
        return await _context.Users
          .Where(user => user.Id == userId)
          .Include(user => user.PlannedDateNotifs)
          .Select(user => user.PlannedDateNotifs)
          .AsNoTracking().SingleOrDefaultAsync() ?? throw new Exception($"PlannedNotifs not found for user {userId}");
    }

    public async Task AddPlannedNotifs()
    {
        DateOnly today = DateOnly.FromDateTime(DateTime.Today);

        var users = await _context.Users
          .AsSplitQuery()
          .Include(user => user.PlannedDateNotifs)
          .Include(user => user.UserWishlists)
          .ThenInclude(list => list.UserListItems.Where(item => item.PlannedDate == today))
          .Include(user => user.UserCustomLists)
          .ThenInclude(list => list.UserListItems.Where(item => item.PlannedDate == today))
          .ToListAsync();

        foreach (var user in users)
        {
            foreach (var userList in user.UserWishlists)
            {
                foreach (var userListItem in userList.UserListItems)
                {
                    PlannedDateNotif notif = new() { ItemId = userListItem.ItemId, ItemTitle = userListItem.ItemTitle, ListName = userList.Name, ListType = userList.ListType, ListContentType = userList.ContentType, PlannedDate = userListItem.PlannedDate, SentTime = DateTime.Now };
                    user.PlannedDateNotifs.Add(notif);
                }
            }
            foreach (var userList in user.UserCustomLists)
            {
                foreach (var userListItem in userList.UserListItems)
                {
                    PlannedDateNotif notif = new() { ItemId = userListItem.ItemId, ItemTitle = userListItem.ItemTitle, ListName = userList.Name, ListType = userList.ListType, ListContentType = userList.ContentType, PlannedDate = userListItem.PlannedDate, SentTime = DateTime.Now };
                    user.PlannedDateNotifs.Add(notif);
                }
            }
        }
        await _context.SaveChangesAsync();
    }

    public async Task ClearPlannedNotif(string userId, int? notifId)
    {
        var user = await _context.Users
          .AsSplitQuery()
          .Where(user => user.Id == userId)
          .Include(user => user.PlannedDateNotifs)
          .SingleOrDefaultAsync() ?? throw new Exception($"User {userId} was not found");

        if (notifId.HasValue)
        {
            var notif = user.PlannedDateNotifs.FirstOrDefault(notif => notif.Id == notifId) ?? throw new Exception($"Planned notification {notifId} was not found");
            user.PlannedDateNotifs.Remove(notif);
        }
        else
        {
            user.PlannedDateNotifs.Clear();
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<RepliedNotif>> GetRepliedNotifs(string userId)
    {
        return await _context.Users
          .Where(user => user.Id == userId)
          .Include(user => user.RepliedNotifs)
          .Select(user => user.RepliedNotifs)
          .AsNoTracking().SingleOrDefaultAsync() ?? throw new Exception($"RepliedNotifs not found for user {userId}");
    }

    public async Task AddRepliedNotifs(CommentNotifData notifData)
    {
        if (notifData.UserIdTo == notifData.UserIdFrom) return;
        var user = await _context.Users
          .Include(user => user.RepliedNotifs)
          .SingleOrDefaultAsync(user => user.Id == notifData.UserIdTo) ?? throw new Exception($"User {notifData.UserIdTo} was not found");
        RepliedNotif notif = new() { SentTime = DateTime.Now, UserIdFrom = notifData.UserIdFrom, ItemId = notifData.ItemId, ItemTitle = notifData.ItemTitle, ItemContentType = notifData.ItemContentType, ReplyId = notifData.textId, ReplyText = notifData.text, ReplyTime = notifData.textTime };
        user.RepliedNotifs.Add(notif);
        await _context.SaveChangesAsync();
    }

    public async Task ClearRepliedNotif(string userId, int? notifId)
    {
        var user = await _context.Users
          .AsSplitQuery()
          .Where(user => user.Id == userId)
          .Include(user => user.RepliedNotifs)
          .SingleOrDefaultAsync() ?? throw new Exception($"User {userId} was not found");

        if (notifId.HasValue)
        {
            var notif = user.RepliedNotifs.FirstOrDefault(notif => notif.Id == notifId) ?? throw new Exception($"Planned notification {notifId} was not found");
            user.RepliedNotifs.Remove(notif);
        }
        else
        {
            user.RepliedNotifs.Clear();
        }
        await _context.SaveChangesAsync();
    }

    public async Task<IEnumerable<FollowedNotif>> GetFollowedNotifs(string userId)
    {
        return await _context.Users
          .Where(user => user.Id == userId)
          .Include(user => user.FollowedNotifs)
          .Select(user => user.FollowedNotifs)
          .AsNoTracking().SingleOrDefaultAsync() ?? throw new Exception($"FollowedNotifs not found for user {userId}");
    }

    public async Task AddFollowedNotifs(CommentNotifData notifData)
    {
        var users = await _context.FollowlistItems.AsSplitQuery()
          .Where(item => item.ItemId == notifData.ItemId && item.Notifications == true)
          .Include(item => item.UserList)
          .Where(item => item.UserList.ContentType == notifData.ItemContentType)
          .Include(item => item.UserList.ApplicationUser.FollowedNotifs)
          .Where(item => item.UserList.ApplicationUserId != notifData.UserIdFrom)
          .Select(item => item.UserList.ApplicationUser).ToListAsync();
        foreach (var user in users)
        {
            FollowedNotif notif = new() { SentTime = DateTime.Now, UserIdFrom = notifData.UserIdFrom, ItemId = notifData.ItemId, ItemTitle = notifData.ItemTitle, ItemContentType = notifData.ItemContentType, CommentId = notifData.textId, CommentText = notifData.text, CommentTime = notifData.textTime };
            user.FollowedNotifs.Add(notif);
        }
        await _context.SaveChangesAsync();
    }

    public async Task ClearFollowedNotif(string userId, int? notifId)
    {
        var user = await _context.Users
          .AsSplitQuery()
          .Where(user => user.Id == userId)
          .Include(user => user.FollowedNotifs)
          .SingleOrDefaultAsync() ?? throw new Exception($"User {userId} was not found");

        if (notifId.HasValue)
        {
            var notif = user.FollowedNotifs.FirstOrDefault(notif => notif.Id == notifId) ?? throw new Exception($"Planned notification {notifId} was not found");
            user.FollowedNotifs.Remove(notif);
        }
        else
        {
            user.FollowedNotifs.Clear();
        }
        await _context.SaveChangesAsync();
    }

    public async Task Remove(string userId, NotifType notifType, int? notifId)
    {
        switch (notifType)
        {
            case NotifType.None: throw new NotImplementedException();
            case NotifType.Planned:
                await ClearPlannedNotif(userId, notifId);
                break;
            case NotifType.Replied:
                await ClearRepliedNotif(userId, notifId);
                break;
            case NotifType.Followed:
                await ClearFollowedNotif(userId, notifId);
                break;
            default: throw new Exception("Invalid NotifType");
        }
    }
}
