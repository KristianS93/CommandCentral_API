using System.Security.Claims;
using Application.Contracts.GroceryList;
using Application.Contracts.Identity;
using Application.Features.GroceryList.Commands.CreateGroceryList;
using Application.Features.GroceryList.Commands.DeleteGroceryList;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
// Permission set to owner as this controller only has Create and Delete
[CustomAuthorize(Permission.Owner)]
public class GroceryListController : ControllerBase
{
    private readonly Mediator _mediatr;
    private readonly IClaimAuthorizationService _claimAuthorizationService;
    private readonly IUserService _userService;

    public GroceryListController(Mediator mediatr, IClaimAuthorizationService claimAuthorizationService, IUserService userService)
    {
        _mediatr = mediatr;
        _claimAuthorizationService = claimAuthorizationService;
        _userService = userService;
    }

    [HttpPost]
    public async Task<ActionResult> CreateList()
    {
        var householdId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        var userId = User.FindFirstValue("uid");
        ArgumentNullException.ThrowIfNull(userId);
        
        var command = new CreateGroceryListCommand(householdId);
        var list = await _mediatr.Send(command);
        
        await _userService.CreateGroceryList(list.Id, userId);

        return Created(nameof(CreateList), list);
    }

    [HttpDelete]
    public async Task<ActionResult> DeleteList()
    {
        var householdId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        var groceryListId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
        
        var userId = User.FindFirstValue("uid");
        ArgumentNullException.ThrowIfNull(userId);
        await _userService.DeleteGroceryList(groceryListId, userId);
        
        var command = new DeleteGroceryListCommand(groceryListId, householdId);
        await _mediatr.Send(command);
        
        return NoContent();
    }

}