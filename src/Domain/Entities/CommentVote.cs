using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class CommentVote
{
    [Key]
    public int CommentVoteId { get; set; }
    public PersonalVote PersonalVote { get; set; }

    public int CommentId { get; set; }
    public Comment Comment { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}