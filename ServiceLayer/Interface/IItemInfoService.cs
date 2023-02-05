using System.Security.Claims;

namespace UserListsMVC.ServiceLayer.Interface;

public interface IItemInfoService
{
  public Task<ItemInfo> Get(string itemId, string userId);
  public Task UpdateItemVote(ClaimsPrincipal? claimsPrincipal, int itemInfoId, PersonalVote personalVote);
  public Task AddComment(ClaimsPrincipal? claimsPrincipal, int itemInfoId, string message);
  public Task RemoveComment(int commentId);
  public Task EditComment(int commentId, string commentText);
  public Task UpdateCommentVote(ClaimsPrincipal? claimsPrincipal, int commentId, PersonalVote personalVote);
  public Task AddReply(ClaimsPrincipal? claimsPrincipal, int commentId, string message);
  public Task RemoveReply(int replyId);
  public Task EditReply(int replyId, string replyText);
  public Task UpdateReplyVote(ClaimsPrincipal? claimsPrincipal, int replyId, PersonalVote personalVote);
  public Task UpdateViewCounter(string userId, int itemInfoId);
  public Task<int> GetCounter(int itemInfoId);
}
