using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class ReplyVote
{
    [Key]
    public int ReplytVoteId { get; set; }
    public PersonalVote PersonalVote { get; set; }

    public int ReplyId { get; set; }
    public Reply Reply { get; set; } = null!;
    public string ApplicationUserId { get; set; } = null!;
    public ApplicationUser ApplicationUser { get; set; } = null!;
}
