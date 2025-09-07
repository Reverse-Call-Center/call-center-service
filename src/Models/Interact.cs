namespace call_center_service.Models;

public record Interact
{
    public required string Type { get; set; }
    public required string Data { get; set; }
}