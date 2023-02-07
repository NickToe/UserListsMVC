namespace UserListsMVC.Events;

public class RepliedNotif : BaseNotif
{
  public string UserIdFrom { get; set; } = null!;
  public string ItemId { get; set; } = null!;
  public string ItemTitle { get; set; } = null!;
  public ContentType ItemContentType { get; set; }
  public int ReplyId { get; set; }
  public string ReplyText { get; set; } = null!;
  public DateTime ReplyTime { get; set; }
}