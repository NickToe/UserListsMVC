using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class Reply
{
  [Key]
  public int ReplyId { get; set; }
  public string Message { get; set; } = null!;
  public DateTime Timestamp { get; set; } = DateTime.Now;
  public bool isEdited { get; set; } = false;
  public ICollection<ReplyVote> ReplyVotes { get; set; } = new List<ReplyVote>();

  public int CommentId { get; set; }
  public Comment Comment { get; set; } = null!;
  public string ApplicationUserId { get; set; } = null!;
  public ApplicationUser ApplicationUser { get; set; } = null!;
}