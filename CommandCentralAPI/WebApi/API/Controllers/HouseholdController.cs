using System.Security.Authentication;
using System.Security.Claims;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.ErrorResponses;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseholdController : ControllerBase
{
    private readonly ILogger<HouseholdController> _logger;
    private readonly IHouseholdService _household;
    private readonly IClaimAuthorizationService _claimAuthorization;

    public HouseholdController(ILogger<HouseholdController> logger, IHouseholdService household, IClaimAuthorizationService claimAuthorization)
    {
        _logger = logger;
        _household = household;
        _claimAuthorization = claimAuthorization;
    }

    [HttpGet]
    [CustomAuthorize(Permission.SiteAdmin)]
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
    [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult<HouseholdEntity>> GetHousehold(int id)
    {
        try
        {
            _claimAuthorization.ConfirmHouseholdClaim(User.FindFirstValue(Claims.Household.ToString())!, id);
            return Ok(await _household.GetByIdAsync(id));
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
        catch (HouseholdDoesNotExistException e)
        {
            var error = new HouseHoldErrors().HouseholdDoesNotExist(id, ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }

    // Consider how access to this should be
    [HttpPost("{name}")]
    [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<ActionResult<HouseholdEntity>> CreateHousehold(string name)
    {
        var household= await _household.CreateAsync(name);
        return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{household.Id}", household);
    }

    [HttpPut("{id}")]
    [CustomAuthorize(Permission.HouseholdAdmin)]
    public async Task<IActionResult> UpdateHousehold(int id, HouseholdEntity item)
    {
        try
        {
            _claimAuthorization.ConfirmHouseholdClaim(User.FindFirstValue(Claims.Household.ToString())!, id);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
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
    [CustomAuthorize(Permission.SiteAdmin)]
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