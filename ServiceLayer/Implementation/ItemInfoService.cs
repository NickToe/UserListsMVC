using System.Security.Claims;
using UserListsMVC.DataLayer.Entities;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.ServiceLayer.Implementation;

public class ItemInfoService : IItemInfoService
{
  private readonly ILogger<ItemInfoService> _logger;
  private readonly IItemInfoRepo _itemInfoRepo;
  private readonly IUserRepo _userRepo;
  private readonly ITextRepo<Comment> _commentRepo;
  private readonly ITextRepo<Reply> _replyRepo;
  private readonly IVoteRepo<ItemVote> _itemVoteRepo;
  private readonly IVoteRepo<CommentVote> _commentVoteRepo;
  private readonly IVoteRepo<ReplyVote> _replyVoteRepo;
  private readonly IViewCounterRepo _viewCounterRepo;
  public ItemInfoService(ILogger<ItemInfoService> logger, IItemInfoRepo itemInfoRepo, IUserRepo userRepo, ITextRepo<Comment> commentRepo, IVoteRepo<ItemVote> itemVoteRepo, ITextRepo<Reply> replyRepo, IVoteRepo<CommentVote> commentVoteRepo, IVoteRepo<ReplyVote> replyVoteRepo, IViewCounterRepo viewCounterRepo)
  {
    _logger = logger;
    _itemInfoRepo = itemInfoRepo;
    _userRepo = userRepo;
    _commentRepo = commentRepo;
    _itemVoteRepo = itemVoteRepo;
    _replyRepo = replyRepo;
    _commentVoteRepo = commentVoteRepo;
    _replyVoteRepo = replyVoteRepo;
    _viewCounterRepo = viewCounterRepo;
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

  public async Task UpdateItemVote(ClaimsPrincipal? claimsPrincipal, int itemInfoId, PersonalVote personalVote)
  {
    string userId = _userRepo.GetId(claimsPrincipal);
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

  public async Task AddComment(ClaimsPrincipal? claimsPrincipal, int itemInfoId, string message)
  {
    Comment comment = new() { ApplicationUserId = _userRepo.GetId(claimsPrincipal), ItemInfoId = itemInfoId, Message = message };
    await _commentRepo.Add(comment);
    await _commentRepo.Save();
  }

  public async Task RemoveComment(int commentId)
  {
    Comment comment = await _commentRepo.Get(commentId);
    _commentRepo.Remove(comment);
    await _commentRepo.Save();
  }

  public async Task AddReply(ClaimsPrincipal? claimsPrincipal, int commentId, string message)
  {
    Reply reply = new() { ApplicationUserId = _userRepo.GetId(claimsPrincipal), CommentId = commentId, Message = message };
    await _replyRepo.Add(reply);
    await _replyRepo.Save();
  }

  public async Task RemoveReply(int replyId)
  {
    Reply reply = await _replyRepo.Get(replyId);
    _replyRepo.Remove(reply);
    await _replyRepo.Save();
  }

  public async Task UpdateCommentVote(ClaimsPrincipal? claimsPrincipal, int commentId, PersonalVote personalVote)
  {
    string userId = _userRepo.GetId(claimsPrincipal);
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

  public async Task UpdateReplyVote(ClaimsPrincipal? claimsPrincipal, int replyId, PersonalVote personalVote)
  {
    string userId = _userRepo.GetId(claimsPrincipal);
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