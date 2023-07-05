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

    [HttpGet("{id}")]
    public async Task<ActionResult<List<GroceryListEntity>>> GetGroceryListItems(int id)
    {
        var itemList = await _groceryListItem.GetAllAsync(id);
        if (itemList == null)
        {
            return NotFound();
        }

        return Ok(itemList);
    }
}