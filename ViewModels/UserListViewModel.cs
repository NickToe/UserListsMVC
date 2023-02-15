using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;

namespace UserListsMVC.ViewModels;

public class UserListViewModel
{
    public UserListViewModel(string userName, ContentType contentType = ContentType.None)
    {
        UserName = userName;
        ContentType = contentType;
    }
    public string UserName { get; set; } = null!;
    public ContentType ContentType { get; set; }
    public ISet<UserList<CustomListItem>> CustomLists { get; set; } = new HashSet<UserList<CustomListItem>>();
    public ISet<UserList<FollowlistItem>> Followlists { get; set; } = new HashSet<UserList<FollowlistItem>>();
    public ISet<UserList<WishlistItem>> Wishlists { get; set; } = new HashSet<UserList<WishlistItem>>();
}