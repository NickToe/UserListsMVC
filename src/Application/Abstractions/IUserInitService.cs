using Domain;

namespace Application.Abstractions;

public interface IUserInitService
{
    public void InitUser(ApplicationUser user);
}
