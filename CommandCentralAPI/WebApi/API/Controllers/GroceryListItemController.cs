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
    
    [HttpGet("GroceryList/{grocerylist_id}/Item/{grocerylist_item_id}")]
    public async Task<ActionResult<GroceryListEntity>> GetByGroceryListItemId(int grocerylist_id, int grocerylist_item_id)
    {
        try
        {
            _claimAuthorization.ConfirmGroceryListClaim(User.FindFirstValue(Claims.GroceryList.ToString())!, grocerylist_id);
            _claimAuthorization.ConfirmItemOnList(grocerylist_id, grocerylist_item_id);
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

    [HttpPost("[controller]/{grocerylist_id}")]
    public async Task<ActionResult> CreateItem(int grocerylist_id, GroceryListItemEntity item)
    {
        item.GroceryListId = grocerylist_id;
        try
        {
            _claimAuthorization.ConfirmGroceryListClaim(User.FindFirstValue(Claims.GroceryList.ToString())!, grocerylist_id);
            await _groceryListItem.CreateAsync(item);
            //created
            return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{item.GroceryListItemId}", null);
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(item.GroceryListId,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
        catch (AuthenticationException)
        {
            var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
            return Unauthorized(error);
        }
    }

    [HttpPut("[controller]/{grocerylist_id}")]
    public async Task<ActionResult> UpdateItem(int grocerylist_id, GroceryListItemEntity item)
    {
        item.GroceryListId = grocerylist_id;
        try
        {
            _claimAuthorization.ConfirmGroceryListClaim(User.FindFirstValue(Claims.GroceryList.ToString())!, grocerylist_id);
            _claimAuthorization.ConfirmItemOnList(grocerylist_id, item.GroceryListItemId);
            await _groceryListItem.UpdateAsync(item);
            return NoContent();
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(item.GroceryListId,
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
    
    [HttpDelete("GroceryList/{grocerylist_id}/Item/{grocerylist_item_id}")]
    public async Task<ActionResult> DeleteItem(int grocerylist_id, int grocerylist_item_id)
    {
        try
        {
            _claimAuthorization.ConfirmGroceryListClaim(User.FindFirstValue(Claims.GroceryList.ToString())!, grocerylist_id);
            _claimAuthorization.ConfirmItemOnList(grocerylist_id, grocerylist_item_id);
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