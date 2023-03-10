using System.Security.Claims;
using Domain;

namespace Application.Abstractions;

public interface IUserService
{
    public Task<IEnumerable<ApplicationUser>> GetAllByUserName(string userName);
    public Task<IEnumerable<ApplicationUser>> GetAll();
    public Task<ApplicationUser> Get(ClaimsPrincipal? claimsUser);
    public Task<ApplicationUser> Get(string userName);
    public Task<ApplicationUser> GetById(string id);
    public Task<bool> Update(string userName);
    public Task<bool> Update(ApplicationUser user);
    public string GetUserName(ClaimsPrincipal? claimsUser);
    public Task<string> GetUserName(string id);
    public string GetId(ClaimsPrincipal? claimsUser);
}