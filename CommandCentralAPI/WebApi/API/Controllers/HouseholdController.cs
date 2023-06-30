using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseholdController : ControllerBase
{
    private readonly ILogger<HouseholdController> _logger;
    private readonly IHouseholdService _household;

    public HouseholdController(ILogger<HouseholdController> logger, IHouseholdService household)
    {
        _logger = logger;
        _household = household;
    }

    [HttpGet]
    public async Task<List<HouseholdEntity>> GetHouseholds()
    {
        try
        {
            return await _household.GetAllAsync();
        }
        catch (ArgumentNullException arg)
        {
            throw;
        }
    }
}