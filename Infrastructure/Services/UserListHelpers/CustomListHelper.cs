using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application;
using UserListsMVC.Domain.Entities;

namespace UserListsMVC.Infrastructure.Services.UserListHelpers;

public class CustomListHelper : UserListBaseHelper<CustomListItem>, IUserListHelper<CustomListItem>
{
    private readonly ILogger<CustomListItem> _logger;

    public CustomListHelper(ILogger<CustomListItem> logger, UserManager<ApplicationUser> userManager) : base(logger, userManager)
    {
        _logger = logger;
    }

    public override async Task<UserList<CustomListItem>> GetWithItems(string userName, string listName)
    {
        return await _userManager.Users
         .Where(user => user.UserName == userName)
         .Include(user => user.UserCustomLists)
         .ThenInclude(list => list.UserListItems.OrderBy(item => item.Position))
         .Select(user => user.UserCustomLists.Where(list => list.Name == listName).SingleOrDefault())
         .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<UserList<CustomListItem>> Get(string userName, string listName)
    {
        return await _userManager.Users
          .Where(user => user.UserName == userName)
          .Include(user => user.UserCustomLists)
          .Select(user => user.UserCustomLists.Where(list => list.Name == listName).SingleOrDefault())
          .SingleOrDefaultAsync() ?? throw new Exception($"UserList '{listName}' was not found for user {userName}");
    }

    public override async Task<ISet<UserList<CustomListItem>>> GetLists(string userName)
    {
        return (await _userManager.Users
          .Include(user => user.UserCustomLists)
          .FirstOrDefaultAsync(user => user.UserName == userName) ?? throw new Exception($"UserCustomLists were not found for user {userName}")).UserCustomLists;
    }
}