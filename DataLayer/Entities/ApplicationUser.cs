using Microsoft.AspNetCore.Identity;

namespace UserListsMVC.DataLayer.Entities;

public class ApplicationUser : IdentityUser
{
    public ISet<UserList<FollowlistItem>> UserFollowlists { get; set; } = null!;
    public ISet<UserList<WishlistItem>> UserWishlists { get; set; } = null!;
    public ISet<UserList<CustomListItem>> UserCustomLists { get; set; } = null!;
}