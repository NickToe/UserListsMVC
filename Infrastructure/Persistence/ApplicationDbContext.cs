using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application;
using UserListsMVC.Domain.Entities;

namespace UserListsMVC.Infrastructure.Persistence;
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<ItemInfo> Items { get; set; } = null!;
    public DbSet<ItemVote> ItemVotes { get; set; } = null!;

    public DbSet<Comment> Comments { get; set; } = null!;
    public DbSet<CommentVote> CommentVotes { get; set; } = null!;
    public DbSet<Reply> Replies { get; set; } = null!;
    public DbSet<ReplyVote> ReplyVotes { get; set; } = null!;

    public DbSet<ViewCounter> ViewCounters { get; set; } = null!;

    public DbSet<FollowlistItem> FollowlistItems { get; set; } = null!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}