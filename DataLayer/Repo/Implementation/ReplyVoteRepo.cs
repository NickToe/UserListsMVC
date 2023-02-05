using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class ReplyVoteRepo : IVoteRepo<ReplyVote>
{
  private readonly ILogger<ReplyVoteRepo> _logger;
  private readonly ApplicationDbContext _context;

  public ReplyVoteRepo(ILogger<ReplyVoteRepo> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<bool> Any(string userId, int parentId) =>
    await _context.ReplyVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.ReplyId == parentId);

  public async Task<ReplyVote> Get(string userId, int parentId) =>
    await _context.ReplyVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.ReplyId == parentId) ?? throw new Exception($"Vote with userId {userId} and replyId {parentId} was not found");

  public async Task Save() =>
    await _context.SaveChangesAsync();

  public async Task Add(ReplyVote vote) =>
    await _context.AddAsync(vote);
}