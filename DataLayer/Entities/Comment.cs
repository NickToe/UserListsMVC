namespace UserListsMVC.DataLayer.Entities;

public class Comment
{
  public int CommentId { get; set; }
  public int ParentId { get; set; } = 0;
  public string UserId { get; set; } = null!;
  public string Message { get; set; } = null!;
  public DateTime Timestamp { get; set; }
  public int VotesUp { get; set; }
  public int VotesDown { get; set; }
  public ICollection<Comment>? Replies { get; set; }
}
