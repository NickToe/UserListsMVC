using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class ViewCounterRepo : IViewCounterRepo
{
    private readonly ILogger<ViewCounterRepo> _logger;
    private readonly ApplicationDbContext _context;

    public ViewCounterRepo(ILogger<ViewCounterRepo> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<bool> Any(string userId, int itemInfoId) =>
      await _context.ViewCounters.AnyAsync(counter => counter.UserIds.Any(id => id == userId) && counter.ItemInfoId == itemInfoId);

    public async Task<int> GetCounter(int itemInfoId) =>
      await _context.ViewCounters.Where(counter => counter.ItemInfoId == itemInfoId).Select(counter => counter.Counter).SingleOrDefaultAsync();

    public async Task<ViewCounter> Get(int itemInfoId) =>
      await _context.ViewCounters.SingleOrDefaultAsync(counter => counter.ItemInfoId == itemInfoId) ?? throw new Exception($"ViewCounter for itemInfo {itemInfoId} was not found");

    public async Task Save() =>
      await _context.SaveChangesAsync();
}
