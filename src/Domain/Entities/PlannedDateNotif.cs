using Domain.Enums;

namespace Domain.Entities;

public class PlannedDateNotif : BaseNotif
{
    public string ItemId { get; set; } = null!;
    public string ItemTitle { get; set; } = null!;
    public string ListName { get; set; } = null!;
    public UserListType ListType { get; set; }
    public ContentType ListContentType { get; set; }
    public DateOnly PlannedDate { get; set; }
}