namespace Domain.Entities;

public class FollowlistItem : UserListItemBase
{
    public FollowlistItem(string itemId, string itemTitle, string itemPoster) : base(itemId, itemTitle, itemPoster) { }
    public FollowlistItem() { }

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
            throw new Exception("This UserList item is not of FollowlistItem type");
        }
    }
}