using CommandCentralAPI.Data;
using Microsoft.AspNetCore.Mvc;

namespace CommandCentralAPI.Controllers;

[Route("[Controller]")]
[ApiController]
public class DbCon : ControllerBase
{
    private readonly DatabaseContext _dbContext;

    public DbCon(DatabaseContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    public async Task<IActionResult> CheckDatabaseConnection()
    {
        try
        {
            // Perform a simple query to check if the database is accessible
            await _dbContext.Database.CanConnectAsync();
            return Ok("Database connection successful.");
        }
        catch
        {
            return StatusCode(500, "Database connection failed.");
        }
    }
    
}