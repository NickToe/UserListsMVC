using UserListsMVC.DataLayer;
using UserListsMVC.DataLayer.Entities;

namespace UserListsMVC.ServiceLayer;

public interface IUserListStore
{
  public Task CreateUserLists(ApplicationUser user);
}

public class UserListStore : IUserListStore
{
  public async Task CreateUserLists(ApplicationUser user)
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
  }
}
