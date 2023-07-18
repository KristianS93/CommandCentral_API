using System.Security.Claims;
using API.Helpers;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models;
using Domain.Models.ErrorResponses;
using Infrastructure.Authentication;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Authorize]
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

    // maybe this method is not needed, as it would only be an admin feature
    [HttpGet]
    public async Task<ActionResult<List<HouseholdEntity>>> GetHouseholds()
    {
        try
        {
            return Ok(await _household.GetAllAsync());
        }
        catch (Exception e)
        {
            // think of a smart way to create an error response here
            // the ToListAsync() can throw 2 exceptions argumentnullexception
            // and operationcancelledexception
            return NotFound(e.InnerException);
        }
    }
    
    [HttpGet("{id}")]
    public async Task<ActionResult<HouseholdEntity>> GetHousehold(int id)
    {
        try
        {
            ClaimAuthorizationHelper.ConfirmHouseholdClaim(User.FindFirstValue("household"), id);
            return Ok(await _household.GetByIdAsync(id));
        }
        catch (HouseholdDoesNotExistException e)
        {
            var error = new HouseHoldErrors().HouseholdDoesNotExist(id, ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }

    // Consider how access to this should be
    [HttpPost("{name}")]
    public async Task<ActionResult<HouseholdEntity>> CreateHousehold(string name)
    {
        var household= await _household.CreateAsync(name);
        return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{household.Id}", household);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateHousehold(int id, HouseholdEntity item)
    {
        // try
        // {
            ClaimAuthorizationHelper.ConfirmHouseholdClaim(User.FindFirstValue("household"), id);
        // }
        // catch (ArgumentException e)
        // {
        //     
        // }
        // this validation can stay since it does not require db.
        if (id != item.Id)
        {
            var error = new HouseHoldErrors()
                .HouseholdIdDoesNotMatchUpdatedHousehold(id, ControllerContext.ActionDescriptor.ControllerName,item.Id);
            return BadRequest(error);
        }

        await _household.UpdateAsync(item);
        return NoContent();
    }

    // Consider who should be able to delete
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteHousehold(int id)
    {
        try
        {
            await _household.DeleteAsync(id);
            return NoContent();
        }
        catch (HouseholdDoesNotExistException e)
        {
            var error = new HouseHoldErrors().HouseholdDoesNotExist(id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }
}