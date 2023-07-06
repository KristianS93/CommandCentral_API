using Domain.Entities;
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

    [HttpGet("GetAll/{grocerylist_id}")]
    public async Task<ActionResult<List<GroceryListEntity>>> GetGroceryListItems(int grocerylist_id)
    {
        var itemList = await _groceryListItem.GetAllAsync(grocerylist_id);
        if (itemList == null)
        {
            return NotFound();
        }

        return Ok(itemList);
    }

    [HttpGet("GetOne/{grocerylist_item_id}")]
    public async Task<ActionResult<GroceryListEntity>> GetByGroceryListItemId(int grocerylist_item_id)
    {
        try
        {
            var item = await _groceryListItem.GetByIdAsync(grocerylist_item_id);
            if (item == null)
            {
                return NotFound();
            }

            return Ok(item);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpPost("{grocerylist}")]
    public async Task<ActionResult> CreateItem(int grocerylist, GroceryListItemEntity grocerylist_item)
    {
        // comment.
        grocerylist_item.GroceryListId = grocerylist;
        try
        {
            await _groceryListItem.CreateAsync(grocerylist_item);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
    }

    [HttpDelete("{grocerylist_item_id}")]
    public async Task<ActionResult> DeleteItem(int grocerylist_item_id)
    {
        try
        {
            await _groceryListItem.DeleteAsync(grocerylist_item_id);
            return Ok();
        }
        catch (ArgumentNullException e)
        {
            return NotFound(e.Message);
        }
    }

    [HttpPut("{groceryListItem}")]
    public async Task<ActionResult> UpdateItem(GroceryListItemEntity groceryListItem)
    {
        try
        {
            await _groceryListItem.UpdateAsync(groceryListItem);
            return Ok();
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }
    }
}