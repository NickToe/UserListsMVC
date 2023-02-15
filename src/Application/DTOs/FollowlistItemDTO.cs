namespace Application.DTOs;

public record FollowlistItemDTO : ListItemBaseDTO
{
    public bool Notifications { get; set; }
}
