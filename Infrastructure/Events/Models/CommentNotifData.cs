using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Infrastructure.Events.Models;

public record CommentNotifData(string UserIdTo, string UserIdFrom, string ItemId, string ItemTitle, ContentType ItemContentType, int textId, string text, DateTime textTime);