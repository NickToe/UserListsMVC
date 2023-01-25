using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using UserListsMVC.DataLayer.Models;

namespace UserListsMVC.DataLayer.Entities;

public class FollowlistItem : UserListItemBase
{
    public FollowlistItem(string itemId, string itemTitle, string itemPoster) : base(itemId, itemTitle, itemPoster) { }
    public FollowlistItem(FollowlistUpdateModel model) : base(model.ItemId, model.Position) { Notifications = model.Notifications; }

    public bool Notifications { get; set; } = true;

    public int UserListId { get; set; }
    public UserList<FollowlistItem> UserList { get; set; } = null!;

    public override void Update(UserListItemBase itemCopy)
    {
        if (itemCopy is FollowlistItem copy)
        {
            Notifications = copy.Notifications;
            Position = copy.Position;
        }
        else
        {
            Console.WriteLine($"{nameof(itemCopy)} {itemCopy.GetType().Name} is not a FollowlistItem");
        }
    }
}