using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Entities;

[Index(nameof(Name), IsUnique = true)]
public class Brand
{
    [Key]
    public required Guid BrandId { get; set; }
    [MaxLength(50)]
    public required string Name { get; set; }
    [MaxLength(50)]
    public required string Domain { get; set; }

    public required int ReceiptCount { get; set; }
    public required DateTime CreatedAt { get; set; }
}