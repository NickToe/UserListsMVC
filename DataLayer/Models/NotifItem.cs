namespace UserListsMVC.DataLayer.Models;

public class NotifItem
{
  public string ItemId { get; set; } = null!;
  public string ItemTitle { get; set; } = null!;
  public ContentType ItemContentType { get; set; }
}
