using call_center_service.Persistence;
using call_center_service.Util;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace call_center_service.Controllers.Receipt;

[ApiController]
[Route("[controller]")]
public class ReceiptController(AppDbContext _dbContext, ILogger<ReceiptController> _logger) : Controller
{
    [HttpPost("create")]
    public async Task<IActionResult> Create(long userId)
    {
        _logger.LogInformation("Getting brands from database");
        var brands = await _dbContext.Brands.ToListAsync();
        _logger.LogInformation("Brands count: {Count}", brands.Count);
        if (brands.Count == 0)
            return StatusCode(422, "No brands found");
        
        var random = new Random();
        var brand = brands[random.Next(brands.Count)];
        
        var receipt = new Entities.Receipt
        {
            CreatorId = userId,
            BrandId = brand.BrandId,
            TransactionId = GenerateUtil.GenerateRandomCode(12)
        };

        try
        {
            _dbContext.Receipts.Add(receipt);
            await _dbContext.SaveChangesAsync();
            await _dbContext.Entry(receipt).ReloadAsync();
            _logger.LogInformation("Receipt created with id: {ReceiptId}", receipt.ReceiptId);
            return Ok(receipt);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "Error creating receipt");
            return StatusCode(500, "Error creating receipt");
        }
    }
}