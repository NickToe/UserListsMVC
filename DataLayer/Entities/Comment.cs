using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class Comment
{
    [Key]
    public int CommentId { get; set; }
    public string Message { get; set; } = null!;
    public DateTime Timestamp { get; set; } = DateTime.Now;
    public bool isEdited { get; set; } = false;
    public ICollection<CommentVote> CommentVotes { get; set; } = new List<CommentVote>();
    public ICollection<Reply> Replies { get; set; } = new List<Reply>();

    public int ItemInfoId { get; set; }
    public ItemInfo ItemInfo { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}