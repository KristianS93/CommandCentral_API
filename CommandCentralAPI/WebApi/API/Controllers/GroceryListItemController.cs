using System.Security.Authentication;
using System.Security.Claims;
using Domain.Entities;
using Domain.Exceptions.GroceryList;
using Domain.Models.ErrorResponses;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[CustomAuthorize(Permission.Member)] // Member should be able to do everything here. ....
public class GroceryListItemController : ControllerBase
{
    private readonly ILogger<GroceryListItemController> _logger;
    private readonly IGroceryListItemService _groceryListItem;
    private readonly IClaimAuthorizationService _claimAuthorization;

    public GroceryListItemController(IGroceryListItemService groceryListItemService, ILogger<GroceryListItemController> logger, IClaimAuthorizationService claimAuthorization)
    {
        _groceryListItem = groceryListItemService;
        _logger = logger;
        _claimAuthorization = claimAuthorization;
    }
    
    [HttpGet("GroceryList/Item/{grocerylist_item_id}")]
    public async Task<ActionResult<GroceryListEntity>> GetByGroceryListItemId(int grocerylist_item_id)
    {
        try
        {
            var id = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
                _claimAuthorization.ConfirmItemOnList(id, grocerylist_item_id);
            var item = await _groceryListItem.GetByIdAsync(grocerylist_item_id);
            return Ok(item);
        }
        catch (ItemDoesNotExistException)
        {
            var error = new GroceryListItemErrors().ItemDoesNotExist(grocerylist_item_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
    }

    [HttpPost("GroceryList/Item/")]
    public async Task<ActionResult> CreateItem(int grocerylist_id, GroceryListItemEntity item)
    {
        try
        {
            item.GroceryListId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
            await _groceryListItem.CreateAsync(item);
            //created
            return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{item.GroceryListItemId}", null);
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
    }

    [HttpPut("GroceryList/Item")]
    public async Task<ActionResult> UpdateItem(GroceryListItemEntity item)
    {
        try
        {
            item.GroceryListId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
            _claimAuthorization.ConfirmItemOnList(item.GroceryListId, item.GroceryListItemId);
            await _groceryListItem.UpdateAsync(item);
            return NoContent();
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (ItemDoesNotExistException)
        {
            var error = new GroceryListItemErrors().ItemDoesNotExist(item.GroceryListItemId,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
    }
    
    [HttpDelete("GroceryList/Item/{grocerylist_item_id}")]
    public async Task<ActionResult> DeleteItem(int grocerylist_item_id)
    {
        try
        {
            var groceryListId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);
            _claimAuthorization.ConfirmItemOnList(groceryListId, grocerylist_item_id);
            await _groceryListItem.DeleteAsync(grocerylist_item_id);
            return NoContent();
        }
        catch (ItemDoesNotExistException)
        {
            var error = new GroceryListItemErrors().ItemDoesNotExist(grocerylist_item_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
    }

}