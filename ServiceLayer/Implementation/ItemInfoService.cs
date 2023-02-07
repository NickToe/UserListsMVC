using UserListsMVC.DataLayer.Entities;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.Events;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.ServiceLayer.Implementation;

public class ItemInfoService : IItemInfoService
{
  private readonly ILogger<ItemInfoService> _logger;
  private readonly IItemInfoRepo _itemInfoRepo;
  private readonly ITextRepo<Comment> _commentRepo;
  private readonly ITextRepo<Reply> _replyRepo;
  private readonly IVoteRepo<ItemVote> _itemVoteRepo;
  private readonly IVoteRepo<CommentVote> _commentVoteRepo;
  private readonly IVoteRepo<ReplyVote> _replyVoteRepo;
  private readonly IViewCounterRepo _viewCounterRepo;
  private readonly IEventProcessor _eventProcessor;
  private readonly IServiceProvider _serviceProvider;
  public ItemInfoService(ILogger<ItemInfoService> logger, IItemInfoRepo itemInfoRepo, ITextRepo<Comment> commentRepo, IVoteRepo<ItemVote> itemVoteRepo, ITextRepo<Reply> replyRepo, IVoteRepo<CommentVote> commentVoteRepo, IVoteRepo<ReplyVote> replyVoteRepo, IViewCounterRepo viewCounterRepo, IEventProcessor eventProcessor, IServiceProvider serviceProvider)
  {
    _logger = logger;
    _itemInfoRepo = itemInfoRepo;
    _commentRepo = commentRepo;
    _itemVoteRepo = itemVoteRepo;
    _replyRepo = replyRepo;
    _commentVoteRepo = commentVoteRepo;
    _replyVoteRepo = replyVoteRepo;
    _viewCounterRepo = viewCounterRepo;
    _eventProcessor = eventProcessor;
    _serviceProvider = serviceProvider;
  }

  public async Task<ItemInfo> Get(string itemId, string userId)
  {
    if (!await _itemInfoRepo.Any(itemId))
    {
      ItemInfo itemInfo = new() { ItemId = itemId };
      await _itemInfoRepo.Add(itemInfo);
      await _itemInfoRepo.Save();
    }
    await _itemInfoRepo.UpdateViewCounter(itemId, userId);
    return await _itemInfoRepo.Get(itemId);
  }

  public async Task UpdateViewCounter(string userId, int itemInfoId)
  {
    if(!await _viewCounterRepo.Any(userId, itemInfoId))
    {
      ViewCounter viewCounter = await _viewCounterRepo.Get(itemInfoId);
      viewCounter.Counter++;
      viewCounter.UserIds.Add(userId);
      await _viewCounterRepo.Save();
    }
  }

  public Task<int> GetCounter(int itemInfoId)
  {
    return _viewCounterRepo.GetCounter(itemInfoId);
  }

  public async Task UpdateItemVote(string userId, int itemInfoId, PersonalVote personalVote)
  {
    if (!await _itemVoteRepo.Any(userId, itemInfoId))
    {
      ItemVote itemVote = new() { ApplicationUserId = userId, ItemInfoId = itemInfoId, PersonalVote = personalVote };
      await _itemVoteRepo.Add(itemVote);
    }
    else
    {
      ItemVote itemVote = await _itemVoteRepo.Get(userId, itemInfoId);
      itemVote.PersonalVote = itemVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
    }
    await _itemVoteRepo.Save();
  }

  public async Task AddComment(NotifItem notifItem, string userId, int itemInfoId, string message)
  {
    Comment comment = new() { ApplicationUserId = userId, ItemInfoId = itemInfoId, Message = message };
    await _commentRepo.Add(comment);
    await _commentRepo.Save();
    _eventProcessor.AddEvent(new CommentAddedEvent(_serviceProvider, new(String.Empty, userId, notifItem.ItemId, notifItem.ItemTitle, notifItem.ItemContentType, comment.CommentId, comment.Message, comment.Timestamp)));
  }

  public async Task RemoveComment(int commentId)
  {
    Comment comment = await _commentRepo.Get(commentId);
    _commentRepo.Remove(comment);
    await _commentRepo.Save();
  }

  public async Task AddReply(NotifItem notifItem, string userId, string userIdFor, int commentId, string message)
  {
    Reply reply = new() { ApplicationUserId = userId, CommentId = commentId, Message = message };
    await _replyRepo.Add(reply);
    await _replyRepo.Save();
    _eventProcessor.AddEvent(new ReplyAddedEvent(_serviceProvider, new(userIdFor, userId, notifItem.ItemId, notifItem.ItemTitle, notifItem.ItemContentType, reply.CommentId, reply.Message, reply.Timestamp)));
  }

  public async Task RemoveReply(int replyId)
  {
    Reply reply = await _replyRepo.Get(replyId);
    _replyRepo.Remove(reply);
    await _replyRepo.Save();
  }

  public async Task UpdateCommentVote(string userId, int commentId, PersonalVote personalVote)
  {
    if(!await _commentVoteRepo.Any(userId, commentId))
    {
      CommentVote commentVote = new() { ApplicationUserId = userId, CommentId = commentId, PersonalVote = personalVote };
      await _commentVoteRepo.Add(commentVote);
    }
    else
    {
      CommentVote commentVote = await _commentVoteRepo.Get(userId, commentId);
      commentVote.PersonalVote = commentVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
    }
    await _commentVoteRepo.Save();
  }

  public async Task UpdateReplyVote(string userId, int replyId, PersonalVote personalVote)
  {
    if (!await _replyVoteRepo.Any(userId, replyId))
    {
      ReplyVote replyVote = new() { ApplicationUserId = userId, ReplyId = replyId, PersonalVote = personalVote };
      await _replyVoteRepo.Add(replyVote);
    }
    else
    {
      ReplyVote replyVote = await _replyVoteRepo.Get(userId, replyId);
      replyVote.PersonalVote = replyVote.PersonalVote == personalVote ? PersonalVote.None : personalVote;
    }
    await _replyVoteRepo.Save();
  }

  public async Task EditComment(int commentId, string commentText)
  {
    Comment comment = await _commentRepo.Get(commentId);
    comment.Message = commentText;
    comment.Timestamp = DateTime.Now;
    comment.isEdited = true;
    await _commentVoteRepo.Save();
  }

  public async Task EditReply(int replyId, string replyText)
  {
    Reply reply = await _replyRepo.Get(replyId);
    reply.Message = replyText;
    reply.Timestamp = DateTime.Now;
    reply.isEdited = true;
    await _replyVoteRepo.Save();
  }
}