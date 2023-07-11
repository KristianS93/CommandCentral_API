using Domain.Entities;
using Domain.Exceptions.GroceryList;
using Domain.Models.ErrorResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryListItemController : ControllerBase
{
    private readonly ILogger<GroceryListItemController> _logger;
    private readonly IGroceryListItemService _groceryListItem;

    public GroceryListItemController(IGroceryListItemService groceryListItemService, ILogger<GroceryListItemController> logger)
    {
        _groceryListItem = groceryListItemService;
        _logger = logger;
    }
    
    [HttpGet("{grocerylist_item_id}")]
    public async Task<ActionResult<GroceryListEntity>> GetByGroceryListItemId(int grocerylist_item_id)
    {
        try
        {
            var item = await _groceryListItem.GetByIdAsync(grocerylist_item_id);
            return Ok(item);
        }
        catch (ItemDoesNotExistException)
        {
            var error = new GroceryListItemErrors().ItemDoesNotExist(grocerylist_item_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }

    [HttpPost("{grocerylist_id}")]
    public async Task<ActionResult> CreateItem(int grocerylist_id, GroceryListItemEntity item)
    {
        item.GroceryListId = grocerylist_id;
        try
        {
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
    }

    [HttpPut("{grocerylist_id}")]
    public async Task<ActionResult> UpdateItem(int grocerylist_id, GroceryListItemEntity item)
    {
        item.GroceryListId = grocerylist_id;
        try
        {
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
    }
    
    [HttpDelete("{grocerylist_item_id}")]
    public async Task<ActionResult> DeleteItem(int grocerylist_item_id)
    {
        try
        {
            await _groceryListItem.DeleteAsync(grocerylist_item_id);
            return NoContent();
        }
        catch (ItemDoesNotExistException)
        {
            var error = new GroceryListItemErrors().ItemDoesNotExist(grocerylist_item_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }

}