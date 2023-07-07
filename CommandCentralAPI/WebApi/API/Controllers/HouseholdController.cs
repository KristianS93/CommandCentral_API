using Domain.Entities;
using Domain.Models;
using Domain.Models.ErrorResponses;
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
            // dette skal standardiseres i set eget... så man kun skal provide evt params
            var error = new HouseHoldErrors(id, $"{ControllerContext.ActionDescriptor.ControllerName}/{id}").HouseholdDoesNotExist();
            return NotFound(error);
        }

        return Ok(item);
    }

    [HttpPost]
    public async Task<ActionResult<HouseholdEntity>> CreateHousehold(string name)
    {
        // this requires further testing
        var house = new HouseholdEntity { Name = name };
        house.Id = await _household.CreateAsync(house);
        // _logger.LogCritical("Generated id {id}", id);
        return Ok(house);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousehold(int id, HouseholdEntity item)
    {
        if (id != item.Id)
        {
            var error = new HouseHoldErrors(id, $"{ControllerContext.ActionDescriptor.ControllerName}/{id}")
                .HouseholdIdDoesNotMatchUpdatedHousehold(item.Id);
            return BadRequest(error);
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