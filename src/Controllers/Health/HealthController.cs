using call_center_service.Persistence;
using call_center_service.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Controllers.Health;

[ApiController]
[Route("[controller]")]
public class HealthController(AppDbContext _dbContext, ILogger<ReceiptController> _logger) : Controller
{
    [HttpGet()]
    public async Task<IActionResult> GetAsync()
    {
        _logger.LogInformation("Getting health check status");
        return Ok("Healthy");
    }
}
