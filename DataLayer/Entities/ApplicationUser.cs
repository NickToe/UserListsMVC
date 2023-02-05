using Microsoft.AspNetCore.Identity;

namespace UserListsMVC.DataLayer.Entities;

public class ApplicationUser : IdentityUser
{
  public ISet<UserList<FollowlistItem>> UserFollowlists { get; set; } = null!;
  public ISet<UserList<WishlistItem>> UserWishlists { get; set; } = null!;
  public ISet<UserList<CustomListItem>> UserCustomLists { get; set; } = null!;

  public ICollection<ItemVote> ItemVotes { get; set; } = null!;

  public ICollection<CommentVote> CommentVotes { get; set; } = null!;
  public ICollection<Comment> Comments { get; set; } = null!;

  public ICollection<ReplyVote> ReplyVotes { get; set; } = null!;
  public ICollection<Reply> Replies { get; set; } = null!;
}