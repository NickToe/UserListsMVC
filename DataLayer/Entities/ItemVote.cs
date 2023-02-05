using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class ItemVote
{
  [Key]
  public int ItemVoteId { get; set; }
  public PersonalVote PersonalVote { get; set; }

  public int ItemInfoId { get; set; }
  public ItemInfo ItemInfo { get; set; } = null!;
  public string ApplicationUserId { get; set; } = null!;
  public ApplicationUser ApplicationUser { get; set; } = null!;
}
