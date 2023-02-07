namespace UserListsMVC.Events;

public class FollowedNotif : BaseNotif
{
  public string UserIdFrom { get; set; } = null!;
  public string ItemId { get; set; } = null!;
  public string ItemTitle { get; set; } = null!;
  public ContentType ItemContentType { get; set; }
  public int CommentId { get; set; }
  public string CommentText { get; set; } = null!;
  public DateTime CommentTime { get; set; }
}
