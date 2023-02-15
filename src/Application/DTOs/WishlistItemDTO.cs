using Domain.Enums;

namespace Application.DTOs;

public record WishlistItemDTO : ListItemBaseDTO
{
    public DateOnly PlannedDate { get; set; }
    public DateOnly StartedDate { get; set; }
    public DateOnly FinishedDate { get; set; }
    public ItemStatus ItemStatus { get; set; }
    public int PersonalScore { get; set; }
    public PersonalVote PersonalVote { get; set; }
};