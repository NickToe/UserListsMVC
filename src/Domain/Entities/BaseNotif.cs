using System.ComponentModel.DataAnnotations;

namespace Domain.Entities;

public class BaseNotif
{
    [Key]
    public int Id { get; set; }
    public DateTime SentTime { get; set; } = DateTime.Now;
}
