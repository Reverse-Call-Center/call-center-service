using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Entities;

[Index(nameof(SessionId), IsUnique = false)]
public class Interaction
{
    [Key]
    public Guid InteractionId { get; set; }
    public required Guid SessionId { get; set; }
    [MaxLength(20)]
    public required string InteractionType { get; set; }
    [MaxLength(100000)]
    public required string InteractionData { get; set; }
    public DateTime InteractionTime { get; set; }
}