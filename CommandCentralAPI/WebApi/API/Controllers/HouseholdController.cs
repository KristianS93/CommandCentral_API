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

    [HttpGet("{id}")]
    public async Task<ActionResult<HouseholdEntity>> GetHousehold(int id)
    {
        var item = await _household.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult> CreateHousehold(HouseholdEntity item)
    {
        // this requires further testing
        var id = await _household.CreateAsync(item);
        _logger.LogCritical("Generated id {id}", id);
        return CreatedAtAction(nameof(GetHousehold), new { id = id }, item);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousehold(int id, HouseholdEntity item)
    {
        if (id != item.HouseholdID)
        {
            return BadRequest();
        }

        await _household.UpdateAsync(item);
        return NoContent();
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteHousehold(int id)
    {
        // test if this get is neccessary.
        var item = await _household.GetByIdAsync(id);
        if (item == null)
        {
            return NotFound();
        }

        await _household.DeleteAsync(item);
        return NoContent();
    }
}