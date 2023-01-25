namespace UserListsMVC.DataLayer.Models;

public record FollowlistUpdateModel
{
  public string ItemId { get; set; } = null!;
  public int Position { get; set; }
  public bool Notifications { get; set; }
}
