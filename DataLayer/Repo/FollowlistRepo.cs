using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace UserListsMVC.DataLayer.Repo;

public class FollowlistRepo : UserListBaseRepo<FollowlistItem>, IUserListRepo<FollowlistItem>
{
  private readonly ILogger<FollowlistRepo> _logger;

  public FollowlistRepo(ILogger<FollowlistRepo> logger, UserManager<ApplicationUser> userManager) : base(logger, userManager)
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