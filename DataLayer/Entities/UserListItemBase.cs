using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace UserListsMVC.DataLayer.Entities;

[Index(nameof(ItemId))]
public abstract class UserListItemBase
{
  public UserListItemBase(string itemId, string itemTitle, string itemPoster)
  {
    ItemId = itemId;
    ItemTitle = itemTitle;
    ItemPoster = itemPoster;
  }

  public UserListItemBase(string itemId, int position)
  {
    ItemId = itemId;
    Position = position;
  }

  public UserListItemBase() { }

  [Key]
  public int ListItemId { get; set; }
  public string ItemId { get; set; } = null!;
  public string ItemTitle { get; set; } = string.Empty;
  public string ItemPoster { get; set; } = string.Empty;
  public int Position { get; set; }
  public DateOnly DateAdded { get; set; } = DateOnly.FromDateTime(DateTime.Now);

  public abstract void Update(UserListItemBase itemCopy);
}