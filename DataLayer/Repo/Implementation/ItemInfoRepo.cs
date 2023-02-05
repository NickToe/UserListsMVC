using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class ItemInfoRepo : IItemInfoRepo
{
  private readonly ILogger<ItemInfoRepo> _logger;
  private readonly ApplicationDbContext _context;

  public ItemInfoRepo(ILogger<ItemInfoRepo> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<ItemInfo> Get(string itemId)
  {
    return await _context.Items
      .AsSplitQuery()
      .Include(item => item.Comments)
      .ThenInclude(comments => comments.Replies)
      .ThenInclude(replies => replies.ReplyVotes)
      .Include(item => item.Comments)
      .ThenInclude(comments => comments.CommentVotes)
      .Include(item => item.Votes)
      .Include(item => item.PageViewCounter)
      .AsNoTracking().SingleOrDefaultAsync(item => item.ItemId == itemId) ?? throw new Exception($"No ItemInfo was found with id {itemId}");
  }

  public async Task<bool> Any(string itemId) =>
    await _context.Items.AnyAsync(item => item.ItemId == itemId);

  public async Task Add(ItemInfo itemInfo) =>
    await _context.Items.AddAsync(itemInfo);

  public async Task UpdateViewCounter(string itemId, string userId)
  {
    ItemInfo itemInfo = await _context.Items.Include(item => item.PageViewCounter).SingleOrDefaultAsync(item => item.ItemId == itemId) ?? throw new Exception($"ItemInfo with id {itemId} was not found");
    if (!itemInfo.PageViewCounter.UserIds.Any(ids => ids == userId))
    {
      itemInfo.PageViewCounter.UserIds.Add(userId);
      itemInfo.PageViewCounter.Counter++;
      await Save();
    }
  }

  public async Task Save() =>
    await _context.SaveChangesAsync();
}
