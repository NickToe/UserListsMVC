using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class ViewCounter
{
  [Key]
  public int Id { get; set; }
  public int Counter { get; set; }
  public List<string> UserIds { get; set; } = new List<string>();

  public int ItemInfoId { get; set; }
  public ItemInfo ItemInfo { get; set; } = null!;
}