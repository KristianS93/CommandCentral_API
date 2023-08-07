using System.Security.Claims;
using Application.Contracts.Identity;
using Application.Features.Household.Commands.CreateHousehold;
using Application.Features.Household.Commands.DeleteHousehold;
using Application.Features.Household.Commands.UpdateHousehold;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Queries.GetHousehold;
using Application.Features.Household.Shared;
using Domain.Entities.Household;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseholdController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly IClaimAuthorizationService _claimAuthorizationService;

    public HouseholdController(IMediator mediatr, IClaimAuthorizationService claimAuthorizationService)
    {
        _mediatr = mediatr;
        _claimAuthorizationService = claimAuthorizationService;
    }

    [HttpGet]
    [Route("/Admin/Households")]
    [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<ActionResult<List<HouseholdDetailsDto>>> GetHouseholds()
    {
        var households = await _mediatr.Send(new GetAllHouseholdsQuery());
        return Ok(households);
    }
    
    [HttpGet]
    [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult<HouseholdDto>> GetHousehold()
    {
        var householdId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        return Ok(await _mediatr.Send(new GetHouseholdQuery(householdId)));
    }

    // Consider how access to this should be
    [HttpPost("{name}")]
    [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<ActionResult<HouseholdEntity>> CreateHousehold(string name)
    {
        // only Siteadmin can create households
        // think of a way new registers should be able to associate to a household
        // or create a household.
        var household = await _mediatr.Send(new CreateHouseholdCommand { Name = name });
        return Created(nameof(CreateHousehold), household);
    }

    [HttpPut]
    [CustomAuthorize(Permission.Owner)]
    public async Task<IActionResult> UpdateHousehold(UpdateHouseholdCommand item)
    {
        var householdId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        item.Id = householdId;
        await _mediatr.Send(item);
        return NoContent();
    }

    // Consider who should be able to delete
    [HttpDelete]
    [CustomAuthorize(Permission.Owner)]
    public async Task<IActionResult> DeleteHousehold()
    {
        var householdId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        await _mediatr.Send(new DeleteHouseholdCommand { Id = householdId });
        return NoContent();
    }
}