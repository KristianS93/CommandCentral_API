using System.Security.Claims;
using Application.Contracts.Identity;
using Application.Features.GroceryListItem.Commands.CreateGroceryListItem;
using Application.Features.GroceryListItem.Commands.DeleteGroceryListItem;
using Application.Features.GroceryListItem.Commands.UpdateGroceryListItem;
using Application.Features.GroceryListItem.Queries.GetAllGroceryListItems;
using Application.Features.GroceryListItem.Shared;
using Application.Features.Household.Commands.CreateHousehold;
using Application.Features.Household.Shared;
using Domain.Entities.GroceryList;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[CustomAuthorize(Permission.Member)]
public class GroceryListItemController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly IClaimAuthorizationService _claimAuthorizationService;

    public GroceryListItemController(IMediator mediatr, IClaimAuthorizationService claimAuthorizationService)
    {
        _mediatr = mediatr;
        _claimAuthorizationService = claimAuthorizationService;
    }
    // Getting all items related to a grocery list
    [HttpGet("GroceryList/Items")] // this route is shit
    [CustomAuthorize(Permission.Member)]
    public async Task<List<GroceryListItemDto>> GetGroceryListItems()
    {
        var groceryListId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);

        var data = await _mediatr.Send(new GetAllGroceryListItemsQuery(groceryListId));

        return data;
    }

    [HttpPost("GroceryList/Item")]
    [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult<GroceryListItemDto>> CreateItem(CreateGroceryListItemCommand item)
    {
        var groceryListId = _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
        
        item.GroceryListId = groceryListId;
        var createItem = await _mediatr.Send(item);
        
        return Created(nameof(CreateItem), createItem);
    }

    [HttpPut("GroceryList/Item")]
    [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult> UpdateItem(UpdateGroceryListItemCommand item)
    {
        var groceryListId = _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
        item.GroceryListId = groceryListId;

        await _mediatr.Send(item);
        
        return Ok();
    }

    [HttpDelete("GroceryList/Item/{groceryListItemId}")]
    [CustomAuthorize(Permission.Member)]
    public async Task<ActionResult> DeleteItem(int groceryListItemId)
    {
        var groceryListId = _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
        var deleteCommand = new DeleteGroceryListItemCommand
            { GroceryListItemId = groceryListItemId, GroceryListId = groceryListId };

        await _mediatr.Send(deleteCommand);
        
        return NoContent();
    }
    
}