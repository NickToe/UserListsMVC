using Domain.Enums;

namespace Application.DTOs;

public class ItemDetailsDTO
{
    public string ItemId { get; set; } = null!;
    public string ItemTitle { get; set; } = null!;
    public ContentType ItemContentType { get; set; }
}
