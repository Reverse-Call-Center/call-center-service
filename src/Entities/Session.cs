using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Entities;

[Index(nameof(RedemptionCode), IsUnique = false)]
[Index(nameof(SessionFingerPrint), IsUnique = false)]
[Index(nameof(SessionPhoneNumber), IsUnique = false)]
[Index(nameof(SessionIpAddress), IsUnique = false)]
public class Session
{
    [Key]
    public Guid SessionId { get; set; }
    public int RedemptionCode { get; set; }
    [MaxLength(20)]
    public string? SessionType { get; set; }
    [MaxLength(1000)]
    public string? SessionFingerPrint { get; set; }
    [MaxLength(20)]
    public string? SessionPhoneNumber { get; set; }
    [MaxLength(20)]
    public string? SessionIpAddress { get; set; }
    public DateTime SessionStart { get; set; }
    public DateTime? SessionEnd { get; set; }
}