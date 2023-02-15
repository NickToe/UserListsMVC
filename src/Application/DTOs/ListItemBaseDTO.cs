namespace Application.DTOs;

public record ListItemBaseDTO
{
    public string ItemId { get; set; } = null!;
    public int Position { get; set; }
}
