using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Repo.Interface;

namespace UserListsMVC.DataLayer.Repo.Implementation;

public class CommentRepo : ITextRepo<Comment>
{
    private readonly ILogger<CommentRepo> _logger;
    private readonly ApplicationDbContext _context;

    public CommentRepo(ILogger<CommentRepo> logger, ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public async Task<Comment> Get(int commentId) =>
      await _context.Comments.SingleOrDefaultAsync(comment => comment.CommentId == commentId) ?? throw new Exception($"Comment with id {commentId} not found");

    public async Task Add(Comment comment) =>
      await _context.Comments.AddAsync(comment);

    public void Remove(Comment comment) =>
      _context.Comments.Remove(comment);

    public async Task Save() =>
      await _context.SaveChangesAsync();
}
