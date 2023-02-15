using UserListsMVC.Application.DTOs;
using UserListsMVC.Domain.Entities;
using UserListsMVC.Domain.Enums;

namespace UserListsMVC.Application.Abstractions;

public interface IItemInfoService
{
    public Task<ItemInfo> Get(string itemId, string userId);
    public Task UpdateItemVote(string userId, int itemInfoId, PersonalVote personalVote);
    public Task AddComment(ItemDetailsDTO itemDetailsDTO, string userId, int itemInfoId, string message);
    public Task RemoveComment(int commentId);
    public Task EditComment(int commentId, string commentText);
    public Task UpdateCommentVote(string userId, int commentId, PersonalVote personalVote);
    public Task AddReply(ItemDetailsDTO itemDetailsDTO, string userId, string userIdFor, int commentId, string message);
    public Task RemoveReply(int replyId);
    public Task EditReply(int replyId, string replyText);
    public Task UpdateReplyVote(string userId, int replyId, PersonalVote personalVote);
}
