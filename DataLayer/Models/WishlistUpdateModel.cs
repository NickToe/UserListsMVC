namespace UserListsMVC.DataLayer.Models;

public record WishlistUpdateModel
{
  public string ItemId { get; set; } = null!;
  public int Position { get; set; }
  public DateOnly PlannedDate { get; set; }
  public DateOnly StartedDate { get; set; }
  public DateOnly FinishedDate { get; set; }
  public ItemStatus ItemStatus { get; set; }
  public int PersonalScore { get; set; }
  public PersonalVote PersonalVote { get; set; }
};