using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using UserListsMVC.ServiceLayer.Interface;
using UserListsMVC.Events;

namespace UserListsMVC.Controllers;

[Controller]
[Route("[controller]")]
[Authorize]
public class ItemInfoController : Controller
{
  private readonly ILogger<ItemInfoController> _logger;
  private readonly IItemInfoService _itemInfoService;
  private readonly IEventProcessor _eventProcessor;
  private string UserId => HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value ?? String.Empty;

  public ItemInfoController(ILogger<ItemInfoController> logger, IItemInfoService itemInfoService, IEventProcessor eventProcessor)
  {
    _logger = logger;
    _itemInfoService = itemInfoService;
    _eventProcessor = eventProcessor;
  }

  [HttpGet("UpdateVote")]
  public async Task<IActionResult> UpdateVote(int itemInfoId, PersonalVote personalVote)
  {
    _logger.LogInformation("Changing vote of User {userName} for itemInfo {itemInfoId} to {vote}", User?.Identity?.Name, itemInfoId, personalVote);
    await _itemInfoService.UpdateItemVote(UserId, itemInfoId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("AddComment")]
  public async Task<IActionResult> AddComment(NotifItem notifItem, int itemInfoId, string commentText)
  {
    _logger.LogInformation("Adding comment for User {userName} for item {itemInfoId}: {text}", User?.Identity?.Name, itemInfoId, commentText);
    await _itemInfoService.AddComment(notifItem, UserId, itemInfoId, commentText);
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
    await _itemInfoService.UpdateCommentVote(UserId, commentId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  [HttpGet("AddReply")]
  public async Task<IActionResult> AddReply(NotifItem notifItem, string userIdFor, int commentId, string replyText)
  {
    _logger.LogInformation($"TEST: itemId({notifItem.ItemId}), itemTitle({notifItem.ItemTitle}), itemContentType({notifItem.ItemContentType}), userIdFor({userIdFor})");
    _logger.LogInformation("Adding reply for User {userName} for comment {commentId}: {replyText}", User?.Identity?.Name, commentId, replyText);
    await _itemInfoService.AddReply(notifItem, UserId, userIdFor, commentId, replyText);
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
    await _itemInfoService.UpdateReplyVote(UserId, replyId, personalVote);
    return Redirect(Request.Headers["Referer"].ToString());
  }

  public override async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
  {
    await next();
    await _eventProcessor.ProcessEvents();
  }
}
