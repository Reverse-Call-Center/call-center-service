using call_center_service.Persistence;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Controllers.Session;

[ApiController]
[Route("[controller]")]
public class SessionController(AppDbContext _dbContext, ILogger<SessionController> _logger) : Controller
{
    [HttpPost("start")]
    public async Task<IActionResult> Start(string redemptionToken, string sessionType, string? sessionFingerPrint, string? sessionPhoneNumber, string? sessionIpAddress)
    {
        var code = int.TryParse(redemptionToken, out var redemptionCode);
        if (!code)
        {
            _logger.LogError("Invalid redemption code: {token}", redemptionToken);
            return Unauthorized("Bad redemption code");
        }

        if (redemptionCode < 100000 || redemptionCode > 999999)
        {
            _logger.LogError("Invalid redemption code: {token}", redemptionToken);
            return Unauthorized("Bad redemption code");
        }

        if (!await _dbContext.Receipts.AnyAsync(x => x.RedemptionCode == redemptionCode))
        {
            _logger.LogError("Receipt lookup found no code: {token}", redemptionToken);
            return Unauthorized("Bad redemption code");
        }

        if (!sessionType.Equals("Phone", StringComparison.OrdinalIgnoreCase) &&
            !sessionType.Equals("Web", StringComparison.OrdinalIgnoreCase))
        {
            _logger.LogError("Invalid session type: {type}", sessionType);
            return BadRequest("Invalid session type");
        }
        
        var missingSessionData = true;
        if (!string.IsNullOrWhiteSpace(sessionFingerPrint) && !string.IsNullOrWhiteSpace(sessionIpAddress))
            missingSessionData = false;
        if (missingSessionData && !string.IsNullOrWhiteSpace(sessionPhoneNumber))
            missingSessionData = false;
        
        if (missingSessionData)
            return BadRequest("Invalid session data");

        var session = new Entities.Session
        {
            RedemptionCode = redemptionCode,
            SessionType = sessionType,
            SessionFingerPrint = sessionFingerPrint,
            SessionPhoneNumber = sessionPhoneNumber,
            SessionIpAddress = sessionIpAddress
        };
        
        try
        {
            _dbContext.Sessions.Add(session);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(session).ReloadAsync();
            _logger.LogInformation("Session created with id: {SessionId}", session.SessionId);
            return Ok(session.SessionId.ToString());
            
        } catch (Exception e)
        {
            _logger.LogError(e, "Error creating session");
            return StatusCode(500, "Error creating session");
        }
    }

    [HttpPut("end")]
    public async Task<IActionResult> End(Guid sessionId)
    {
        if(string.IsNullOrWhiteSpace(sessionId.ToString()))
            return BadRequest("Invalid session id");

        try
        {
            var affectedRows = await _dbContext.Database.ExecuteSqlRawAsync(
                "UPDATE \"Sessions\" SET \"SessionEnd\" = NOW() WHERE \"SessionId\" = {0} AND \"SessionEnd\" IS NULL",
                sessionId);
        
            if (affectedRows == 0)
                return NotFound("Session not found");
            if (affectedRows == 1)
                return Ok();
            
            _logger.LogError("Error updating sessionEnd, affected rows: {AffectedRows}", affectedRows);
            return StatusCode(500, "Error updating session");
        } catch (Exception e)
        {
            _logger.LogError(e, "Error updating session");
            return StatusCode(500, "Error updating session");
        }
    }
    
}