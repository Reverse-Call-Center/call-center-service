using call_center_service.Persistence;
using call_center_service.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Controllers.Health;

[ApiController]
[Route("[controller]")]
public class HealthController() : Controller
{
    [HttpGet()]
    public IActionResult Get()
    {
        return Ok("Healthy");
    }
}
