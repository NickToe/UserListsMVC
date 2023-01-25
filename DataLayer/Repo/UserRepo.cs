﻿using Microsoft.AspNetCore.Identity;
using UserListsMVC.DataLayer;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace UserListsMVC.DataLayer.Repo;

public class UserRepo : IUserRepo
{
  private readonly ILogger<UserRepo> _logger;
  private readonly UserManager<ApplicationUser> _userManager;
  public UserRepo(ILogger<UserRepo> logger, UserManager<ApplicationUser> userManager)
	{
    _logger = logger;
    _userManager = userManager;
	}

  public async Task<IEnumerable<ApplicationUser>> GetAllByUserName(string userName) =>
    await _userManager.Users.Where(item => item.UserName.ToLower().Contains(userName.ToLower())).ToListAsync();

  public async Task<IEnumerable<ApplicationUser>> GetAll() =>
    await _userManager.Users.ToListAsync();

  public async Task<ApplicationUser> Get(ClaimsPrincipal? claimsUser) =>
    await _userManager.GetUserAsync(claimsUser) ?? throw new Exception($"User {claimsUser?.Identity?.Name} was not found");

  public async Task<ApplicationUser> Get(string userName) =>
    await _userManager.FindByNameAsync(userName) ?? throw new Exception($"User {userName} was not found");

  public async Task<ApplicationUser> GetById(string id) =>
    await _userManager.FindByIdAsync(id);

  public async Task<bool> Update(string userName) =>
    await Update(await _userManager.FindByNameAsync(userName));

  public async Task<bool> Update(ApplicationUser user)
  {
    bool res = true;
    IdentityResult identityRes = await _userManager.UpdateAsync(user);
    if (!identityRes.Succeeded)
    {
      res = false;
      _logger.LogWarning("Something went wrong while updating the user {UserName}", user.UserName);
      foreach (var error in identityRes.Errors)
      {
        _logger.LogWarning("{Code}: {Description}", error.Code, error.Description);
      }
    }
    else
    {
      _logger.LogInformation("User {UserName} was successfully updated!", user.UserName);
    }
    return res;
  }

  public string GetUserName(ClaimsPrincipal? claimsUser)
  {
    ArgumentNullException.ThrowIfNull(claimsUser, nameof(claimsUser));
    return _userManager.GetUserName(claimsUser);
  }
}
