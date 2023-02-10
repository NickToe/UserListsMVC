using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class WishlistItem : UserListItemBase
{
    public WishlistItem(string itemId, string itemTitle, string itemPoster) : base(itemId, itemTitle, itemPoster) { }
    public WishlistItem(WishlistUpdateModel model) : base(model.ItemId, model.Position)
    {
        PlannedDate = model.PlannedDate;
        StartedDate = model.StartedDate;
        FinishedDate = model.FinishedDate;
        ItemStatus = model.ItemStatus;
        PersonalScore = model.PersonalScore;
        PersonalVote = model.PersonalVote;
    }

    public DateOnly PlannedDate { get; set; } = default;
    public DateOnly StartedDate { get; set; } = default;
    public DateOnly FinishedDate { get; set; } = default;

    [Range(0, 10)]
    public int PersonalScore { get; set; } = 0; // Personal score, doesn't affect total user rating
    public ItemStatus ItemStatus { get; set; } = ItemStatus.NotStarted;
    public PersonalVote PersonalVote { get; set; } = PersonalVote.None;

    public int UserListId { get; set; }
    public UserList<WishlistItem> UserList { get; set; } = null!;

    public override void Update(UserListItemBase itemCopy)
    {
        if (itemCopy is WishlistItem copy)
        {
            Position = copy.Position;
            PlannedDate = copy.PlannedDate;
            StartedDate = copy.StartedDate;
            FinishedDate = copy.FinishedDate;
            PersonalScore = copy.PersonalScore;
            ItemStatus = copy.ItemStatus;
            PersonalVote = copy.PersonalVote;
        }
        else
        {
            throw new Exception("This UserList item is not of WishlistItem type");
        }
    }
}