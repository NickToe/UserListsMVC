using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application;
using UserListsMVC.Domain.Entities;

namespace UserListsMVC.Infrastructure.Services.UserListHelpers;

public class WishlistHelper : UserListBaseHelper<WishlistItem>, IUserListHelper<WishlistItem>
{
    private readonly ILogger<WishlistHelper> _logger;

    public WishlistHelper(ILogger<WishlistHelper> logger, UserManager<ApplicationUser> userManager) : base(logger, userManager)
    {
        _logger = logger;
    }

    public override async Task<UserList<WishlistItem>> GetWithItems(string userName, string listName)
    {
        return await _userManager.Users
         .Where(user => user.UserName == userName)
         .Include(user => user.UserWishlists)
         .ThenInclude(list => list.UserListItems.OrderBy(item => item.Position))
         .Select(user => user.UserWishlists.Where(list => list.Name == listName).SingleOrDefault())
         .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<UserList<WishlistItem>> Get(string userName, string listName)
    {
        return await _userManager.Users
          .Where(user => user.UserName == userName)
          .Include(user => user.UserWishlists)
          .Select(user => user.UserWishlists.Where(list => list.Name == listName).SingleOrDefault())
          .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<ISet<UserList<WishlistItem>>> GetLists(string userName)
    {
        return (await _userManager.Users
          .Include(user => user.UserWishlists)
          .FirstOrDefaultAsync(user => user.UserName == userName) ?? throw new Exception($"UserWishlists were not found for user {userName}")).UserWishlists;
    }
}
