using System.Security.Claims;

namespace UserListsMVC.ViewModels;

public class UserListItemsViewModel<T>
{
    public string UserName { get; set; }
    public T UserList { get; set; }

    public UserListItemsViewModel(string userName, T userList)
    {
        UserName = userName;
        UserList = userList;
    }
    public UserListItemsViewModel(ClaimsPrincipal claimsPrincipal, T userList)
    {
        UserName = claimsPrincipal?.Identity?.Name ?? "NotFound";
        UserList = userList;
    }
}