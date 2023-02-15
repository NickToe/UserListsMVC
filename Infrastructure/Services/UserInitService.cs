using UserListsMVC.Application;
using UserListsMVC.Application.Abstractions;
using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Infrastructure.Services;

public class UserInitService : IUserInitService
{
    public void InitUser(ApplicationUser user)
    {
        user.UserFollowlists = new HashSet<UserList<FollowlistItem>>()
        {
            { new(ContentType.Game, UserListType.Followlist) },
            { new(ContentType.Movie, UserListType.Followlist) },
        };
        user.UserWishlists = new HashSet<UserList<WishlistItem>>()
        {
            { new(ContentType.Game, UserListType.Wishlist) },
            { new(ContentType.Movie, UserListType.Wishlist) },
        };
        user.UserCustomLists = new HashSet<UserList<CustomListItem>>();
        user.ItemVotes = new List<ItemVote>();
        user.Comments = new List<Comment>();
        user.CommentVotes = new List<CommentVote>();
        user.Replies = new List<Reply>();
        user.ReplyVotes = new List<ReplyVote>();
    }
}