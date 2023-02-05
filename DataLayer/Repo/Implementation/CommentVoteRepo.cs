using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class CommentVoteRepo : IVoteRepo<CommentVote>
{
  private readonly ILogger<CommentVoteRepo> _logger;
  private readonly ApplicationDbContext _context;

  public CommentVoteRepo(ILogger<CommentVoteRepo> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<bool> Any(string userId, int parentId) =>
    await _context.CommentVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.CommentId == parentId);

  public async Task Add(CommentVote commentVote) =>
    await _context.CommentVotes.AddAsync(commentVote);

  public async Task<CommentVote> Get(string userId, int parentId) =>
    await _context.CommentVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.CommentId == parentId) ?? throw new Exception($"Vote with userId {userId} and commentId {parentId} was not found");

  public async Task Save() =>
    await _context.SaveChangesAsync();
}
