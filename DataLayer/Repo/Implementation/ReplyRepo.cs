using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class ReplyRepo : ITextRepo<Reply>
{
  private readonly ILogger<ReplyRepo> _logger;
  private readonly ApplicationDbContext _context;

  public ReplyRepo(ILogger<ReplyRepo> logger, ApplicationDbContext context)
  {
    _logger = logger;
    _context = context;
  }

  public async Task<Reply> Get(int replyId) =>
    await _context.Replies.SingleOrDefaultAsync(reply => reply.ReplyId == replyId) ?? throw new Exception($"Reply with id {replyId} was not found");

  public async Task Add(Reply reply) =>
  await _context.Replies.AddAsync(reply);

  public void Remove(Reply reply) =>
    _context.Remove(reply);

  public async Task Save() =>
    await _context.SaveChangesAsync();
}
