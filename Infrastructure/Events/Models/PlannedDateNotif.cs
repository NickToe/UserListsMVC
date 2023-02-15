using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Infrastructure.Events.Models;

public class PlannedDateNotif : BaseNotif
{
    public string ItemId { get; set; } = null!;
    public string ItemTitle { get; set; } = null!;
    public string ListName { get; set; } = null!;
    public UserListType ListType { get; set; }
    public ContentType ListContentType { get; set; }
    public DateOnly PlannedDate { get; set; }
}