using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Entities;

[Index(nameof(CreatorId), IsUnique = false)]
public class Receipt
{
    [Key]
    public Guid ReceiptId { get; set; }
    public required long CreatorId { get; set; }
    public required Guid BrandId { get; set; }
    public DateTime? CreatedAt { get; set; }
    
    [ForeignKey(nameof(BrandId))]
    public Brand? Brand { get; set; }
}