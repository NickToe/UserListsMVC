using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class ItemDetails
{
  [Key]
  public int ItemDetailsId { get; set; }
  public string ItemId { get; set; } = null!;
  public int VotesUp { get; set; }
  public int VotesDown { get; set; }
  public ICollection<Comment> Comments { get; set; } = null!;
}
