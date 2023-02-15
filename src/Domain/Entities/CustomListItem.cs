using System.ComponentModel.DataAnnotations;
using Domain.Enums;

namespace Domain.Entities;

public class CustomListItem : UserListItemBase
{
    public CustomListItem(string itemId, string itemTitle, string itemPoster) : base(itemId, itemTitle, itemPoster) { }
    public CustomListItem() { }

    public DateOnly PlannedDate { get; set; } = default;
    public DateOnly StartedDate { get; set; } = default;
    public DateOnly FinishedDate { get; set; } = default;

    [Range(0, 10)]
    public int PersonalScore { get; set; } = 0; // Personal score, doesn't affect total user rating
    public ItemStatus ItemStatus { get; set; } = ItemStatus.NotStarted;
    public PersonalVote PersonalVote { get; set; } = PersonalVote.None;

    public int UserListId { get; set; }
    public UserList<CustomListItem> UserList { get; set; } = null!;

    public override void Update(UserListItemBase itemCopy)
    {
        if (itemCopy is CustomListItem copy)
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
            throw new Exception("This UserList item is not of CustomListItem type");
        }
    }
}
