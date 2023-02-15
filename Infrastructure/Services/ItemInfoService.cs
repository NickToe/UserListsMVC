using Microsoft.EntityFrameworkCore;
using UserListsMVC.Application.Abstractions;
using UserListsMVC.Application.DTOs;
using UserListsMVC.Application.Events;
using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;
using UserListsMVC.Infrastructure.Events;
using UserListsMVC.Infrastructure.Persistence;

namespace UserListsMVC.Infrastructure.Services;

public class ItemInfoService : IItemInfoService
{
    private readonly ILogger<ItemInfoService> _logger;
    private readonly ApplicationDbContext _context;
    private readonly IEventProcessor _eventProcessor;
    private readonly IServiceProvider _serviceProvider;
    public ItemInfoService(ILogger<ItemInfoService> logger, ApplicationDbContext context, IEventProcessor eventProcessor, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _context = context;
        _eventProcessor = eventProcessor;
        _serviceProvider = serviceProvider;
    }

    public async Task<ItemInfo> Get(string itemId, string userId)
    {
        if (!await _context.Items.AnyAsync(item => item.ItemId == itemId))
        {
            ItemInfo itemInfoAdded = new() { ItemId = itemId };
            await _context.Items.AddAsync(itemInfoAdded);
            await _context.SaveChangesAsync();
        }

        ItemInfo itemInfo = await _context.Items.Include(item => item.PageViewCounter).SingleOrDefaultAsync(item => item.ItemId == itemId) ?? throw new Exception($"ItemInfo with id {itemId} was not found");
        if (!itemInfo.PageViewCounter.UserIds.Any(ids => ids == userId))
        {
            itemInfo.PageViewCounter.UserIds.Add(userId);
            itemInfo.PageViewCounter.Counter++;
            await _context.SaveChangesAsync();
        }

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

    public async Task UpdateItemVote(string userId, int itemInfoId, PersonalVote personalVote)
    {
        if (!await _context.ItemVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.ItemInfoId == itemInfoId))
        {
            ItemVote itemVote = new() { ApplicationUserId = userId, ItemInfoId = itemInfoId, PersonalVote = personalVote };
            await _context.ItemVotes.AddAsync(itemVote);
        }
        else
        {
            ItemVote itemVote = await _context.ItemVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.ItemInfoId == itemInfoId) ?? throw new Exception($"Vote with userId {userId} and itemInfoId {itemInfoId} not found");
            itemVote.PersonalVote = itemVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
        }

        await _context.SaveChangesAsync();
    }

    public async Task AddComment(ItemDetailsDTO itemDetailsDTO, string userId, int itemInfoId, string message)
    {
        Comment comment = new() { ApplicationUserId = userId, ItemInfoId = itemInfoId, Message = message };
        await _context.Comments.AddAsync(comment);
        await _context.SaveChangesAsync();
        _eventProcessor.AddEvent(new CommentAddedEvent(_serviceProvider, new(string.Empty, userId, itemDetailsDTO.ItemId, itemDetailsDTO.ItemTitle, itemDetailsDTO.ItemContentType, comment.CommentId, comment.Message, comment.Timestamp)));
    }

    public async Task RemoveComment(int commentId)
    {
        Comment comment = await _context.Comments.SingleOrDefaultAsync(comment => comment.CommentId == commentId) ?? throw new Exception($"Comment with id {commentId} not found");
        _context.Comments.Remove(comment);
        await _context.SaveChangesAsync();
    }

    public async Task AddReply(ItemDetailsDTO itemDetailsDTO, string userId, string userIdFor, int commentId, string message)
    {
        Reply reply = new() { ApplicationUserId = userId, CommentId = commentId, Message = message };
        await _context.Replies.AddAsync(reply);
        await _context.SaveChangesAsync();
        _eventProcessor.AddEvent(new ReplyAddedEvent(_serviceProvider, new(userIdFor, userId, itemDetailsDTO.ItemId, itemDetailsDTO.ItemTitle, itemDetailsDTO.ItemContentType, reply.CommentId, reply.Message, reply.Timestamp)));
    }

    public async Task RemoveReply(int replyId)
    {
        Reply reply = await _context.Replies.SingleOrDefaultAsync(reply => reply.ReplyId == replyId) ?? throw new Exception($"Reply with id {replyId} was not found");
        _context.Remove(reply);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateCommentVote(string userId, int commentId, PersonalVote personalVote)
    {
        if (!await _context.CommentVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.CommentId == commentId))
        {
            CommentVote commentVote = new() { ApplicationUserId = userId, CommentId = commentId, PersonalVote = personalVote };
            await _context.CommentVotes.AddAsync(commentVote);
        }
        else
        {
            CommentVote commentVote = await _context.CommentVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.CommentId == commentId) ?? throw new Exception($"Vote with userId {userId} and commentId {commentId} was not found");
            commentVote.PersonalVote = commentVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
        }

        await _context.SaveChangesAsync();
    }

    public async Task UpdateReplyVote(string userId, int replyId, PersonalVote personalVote)
    {
        if (!await _context.ReplyVotes.AnyAsync(vote => vote.ApplicationUserId == userId && vote.ReplyId == replyId))
        {
            ReplyVote replyVote = new() { ApplicationUserId = userId, ReplyId = replyId, PersonalVote = personalVote };
            await _context.ReplyVotes.AddAsync(replyVote);
        }
        else
        {
            ReplyVote replyVote = await _context.ReplyVotes.SingleOrDefaultAsync(vote => vote.ApplicationUserId == userId && vote.ReplyId == replyId) ?? throw new Exception($"Vote with userId {userId} and replyId {replyId} was not found");
            replyVote.PersonalVote = replyVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
        }

        await _context.SaveChangesAsync();
    }

    public async Task EditComment(int commentId, string commentText)
    {
        Comment comment = await _context.Comments.SingleOrDefaultAsync(comment => comment.CommentId == commentId) ?? throw new Exception($"Comment with id {commentId} not found");
        comment.Message = commentText;
        comment.Timestamp = DateTime.Now;
        comment.isEdited = true;
        await _context.SaveChangesAsync();
    }

    public async Task EditReply(int replyId, string replyText)
    {
        Reply reply = await _context.Replies.SingleOrDefaultAsync(reply => reply.ReplyId == replyId) ?? throw new Exception($"Reply with id {replyId} was not found");
        reply.Message = replyText;
        reply.Timestamp = DateTime.Now;
        reply.isEdited = true;
        await _context.SaveChangesAsync();
    }
}