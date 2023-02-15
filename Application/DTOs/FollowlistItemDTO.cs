namespace UserListsMVC.Application.DTOs;

public record FollowlistItemDTO
{
    public string ItemId { get; set; } = null!;
    public int Position { get; set; }
    public bool Notifications { get; set; }
}
