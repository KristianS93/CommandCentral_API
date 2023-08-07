using Application.Features.Household.Commands.CreateHousehold;
using Application.Features.Household.Commands.DeleteHousehold;
using Application.Features.Household.Commands.UpdateHousehold;
using Application.Features.Household.Queries.GetAllHouseholds;
using Application.Features.Household.Queries.GetHousehold;
using Application.Features.Household.Shared;
using Domain.Entities.Household;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class HouseholdController : ControllerBase
{
    private readonly IMediator _mediatr;

    public HouseholdController(IMediator mediatr)
    {
        _mediatr = mediatr;
    }

    [HttpGet]
    [Route("/Admin/Households")]
    // [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<ActionResult<List<HouseholdDetailsDto>>> GetHouseholds()
    {
        var households = await _mediatr.Send(new GetAllHouseholdsQuery());
        return Ok(households);
    }
    
    [HttpGet("{id}")]
    // [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult<HouseholdDto>> GetHousehold(int id)
    {
        return Ok(await _mediatr.Send(new GetHouseholdQuery(id)));
    }

    // Consider how access to this should be
    [HttpPost("{name}")]
    // [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<ActionResult<HouseholdEntity>> CreateHousehold(string name)
    {
        var household = await _mediatr.Send(new CreateHouseholdCommand { Name = name });
        return Created(nameof(CreateHousehold), household);
    }

    [HttpPut]
    // [CustomAuthorize(Permission.HouseholdAdmin)]
    public async Task<IActionResult> UpdateHousehold(UpdateHouseholdCommand item)
    {
        await _mediatr.Send(item);
        return NoContent();
    }

    // Consider who should be able to delete
    [HttpDelete("{id}")]
    // [CustomAuthorize(Permission.SiteAdmin)]
    public async Task<IActionResult> DeleteHousehold(int id)
    {
        await _mediatr.Send(new DeleteHouseholdCommand { Id = id });
        return NoContent();
    }
}