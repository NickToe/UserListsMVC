using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application;
using UserListsMVC.Domain.Entities;

namespace UserListsMVC.Infrastructure.Services.UserListHelpers;

public class FollowlistHelper : UserListBaseHelper<FollowlistItem>, IUserListHelper<FollowlistItem>
{
    private readonly ILogger<FollowlistHelper> _logger;

    public FollowlistHelper(ILogger<FollowlistHelper> logger, UserManager<ApplicationUser> userManager) : base(logger, userManager)
    {
        _logger = logger;
    }

    public override async Task<UserList<FollowlistItem>> GetWithItems(string userName, string listName)
    {
        return await _userManager.Users
         .Where(user => user.UserName == userName)
         .Include(user => user.UserFollowlists)
         .ThenInclude(list => list.UserListItems.OrderBy(item => item.Position))
         .Select(user => user.UserFollowlists.Where(list => list.Name == listName).SingleOrDefault())
         .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<UserList<FollowlistItem>> Get(string userName, string listName)
    {
        return await _userManager.Users
          .Where(user => user.UserName == userName)
          .Include(user => user.UserFollowlists)
          .Select(user => user.UserFollowlists.Where(list => list.Name == listName).SingleOrDefault())
          .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<ISet<UserList<FollowlistItem>>> GetLists(string userName)
    {
        return (await _userManager.Users
          .Include(user => user.UserFollowlists)
          .FirstOrDefaultAsync(user => user.UserName == userName) ?? throw new Exception($"UserFollowlists were not found for user {userName}")).UserFollowlists;
    }
}