using System.Text.Json;
using call_center_service.Models;
using call_center_service.Persistence;
using Microsoft.AspNetCore.Mvc;

namespace call_center_service.Controllers.Interaction;

[ApiController]
[Route("[controller]")]
public class InteractionController(AppDbContext _dbContext, ILogger<InteractionController> _logger) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(Guid sessionId)
    {
        if (string.IsNullOrWhiteSpace(sessionId.ToString()))
            return BadRequest("Invalid session id");
        
        if (await _dbContext.Sessions.FindAsync(sessionId) == null)
            return NotFound("Session not found");
        
        var interact = await HttpContext.Request.ReadFromJsonAsync<Interact>();
        
        if (interact == null || string.IsNullOrWhiteSpace(interact.Type) || string.IsNullOrWhiteSpace(interact.Data))
            return BadRequest("Invalid interaction data");
        
        
        var interaction = new Entities.Interaction
        {
            SessionId = sessionId,
            InteractionType = interact.Type,
            InteractionData = interact.Data
        };

        try
        {
            _dbContext.Interactions.Add(interaction);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(interaction).ReloadAsync();
            _logger.LogInformation("Interaction created with id: {InteractionId}", interaction.InteractionId);
            return Ok(interaction.InteractionId);
        } catch (Exception e)
        {
            _logger.LogError(e, "Error creating interaction");
            return StatusCode(500, "Error creating interaction");
        }
    }
}