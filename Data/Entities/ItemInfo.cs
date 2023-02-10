using System.ComponentModel.DataAnnotations;

namespace UserListsMVC.DataLayer.Entities;

public class ItemInfo
{
    [Key]
    public int ItemInfoId { get; set; }
    public string ItemId { get; set; } = null!;
    public ICollection<ItemVote> Votes { get; set; } = new List<ItemVote>();
    public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    public ViewCounter PageViewCounter { get; set; } = new ViewCounter();
}