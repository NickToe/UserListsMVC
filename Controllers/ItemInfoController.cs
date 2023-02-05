using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserListsMVC.DataLayer.Repo.Interface;
using UserListsMVC.ServiceLayer.Interface;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class ItemInfoController : Controller
{
  private readonly ILogger<ItemInfoController> _logger;
  private readonly IItemInfoService _itemInfoService;

  public ItemInfoController(ILogger<ItemInfoController> logger, IItemInfoService itemInfoService)
  {
    _logger = logger;
    _itemInfoService = itemInfoService;
  }

  [HttpGet("UpdateVote")]
  public async Task<IActionResult> UpdateVote(int itemInfoId, PersonalVote personalVote)
  {
    _logger.LogInformation("Changing vote of User {userName} for itemInfo {itemInfoId} to {vote}", User?.Identity?.Name, itemInfoId, personalVote);
    await _itemInfoService.UpdateItemVote(User, itemInfoId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("AddComment")]
  public async Task<IActionResult> AddComment(int itemInfoId, string commentText)
  {
    _logger.LogInformation("Adding comment for User {userName} for item {itemInfoId}: {text}", User?.Identity?.Name, itemInfoId, commentText);
    await _itemInfoService.AddComment(User, itemInfoId, commentText);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("RemoveComment")]
  public async Task<IActionResult> RemoveComment(int commentId)
  {
    _logger.LogInformation("Removing comment {commentId}", commentId);
    await _itemInfoService.RemoveComment(commentId);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("EditComment")]
  public async Task<IActionResult> EditComment(int commentId, string commentText)
  {
    _logger.LogInformation("Editing comment {commentId}", commentId);
    await _itemInfoService.EditComment(commentId, commentText);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("UpdateCommentVote")]
  public async Task<IActionResult> UpdateCommentVote(int commentId, PersonalVote personalVote)
  {
    _logger.LogInformation("Changing vote of User {userName} for commentId {commentId} to {vote}", User?.Identity?.Name, commentId, personalVote);
    await _itemInfoService.UpdateCommentVote(User, commentId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("AddReply")]
  public async Task<IActionResult> AddReply(int commentId, string replyText)
  {
    _logger.LogInformation("Adding reply for User {userName} for comment {commentId}: {replyText}", User?.Identity?.Name, commentId, replyText);
    await _itemInfoService.AddReply(User, commentId, replyText);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("RemoveReply")]
  public async Task<IActionResult> RemoveReply(int replyId)
  {
    _logger.LogInformation("Removing reply {replyId}", replyId);
    await _itemInfoService.RemoveReply(replyId);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("EditReply")]
  public async Task<IActionResult> EditReply(int replyId, string replyText)
  {
    _logger.LogInformation("Editing reply {replyId}", replyId);
    await _itemInfoService.EditReply(replyId, replyText);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("UpdateReplyVote")]
  public async Task<IActionResult> UpdateReplyVote(int replyId, PersonalVote personalVote)
  {
    _logger.LogInformation("Changing vote of User {userName} for replyId {replyId} to {vote}", User?.Identity?.Name, replyId, personalVote);
    await _itemInfoService.UpdateReplyVote(User, replyId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }
}
