using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class ItemVoteRepo : IVoteRepo<ItemVote>
{
    private readonly ILogger<ItemVoteRepo> _logger;
    private readonly ApplicationDbContext _context;
    public ItemVoteRepo(ILogger<ItemVoteRepo> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<bool> Any(string userId, int parentId) =>
      await _context.ItemVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.ItemInfoId == parentId);

    public async Task Add(ItemVote itemVote) =>
      await _context.ItemVotes.AddAsync(itemVote);

    public async Task<ItemVote> Get(string userId, int parentId) =>
      await _context.ItemVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.ItemInfoId == parentId) ?? throw new Exception($"Vote with userId {userId} and itemInfoId {parentId} not found");

    public async Task Save() =>
      await _context.SaveChangesAsync();
}