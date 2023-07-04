using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryListController : ControllerBase
{
    private readonly ILogger<GroceryListController> _logger;
    private readonly IGroceryListService _groceryList;

    public GroceryListController(ILogger<GroceryListController> logger, IGroceryListService groceryList)
    {
        _logger = logger;
        _groceryList = groceryList;
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<GroceryListEntity>> GetGroceryList(int id)
    {
        var groceryList = await _groceryList.GetAsyncByHousehold(id);
        if (groceryList == null)
        {
            return NotFound();
        }

        return Ok(groceryList);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteGroceryList(int id)
    {
        var item = await _groceryList.GetAsyncByHousehold(id);
        if (item == null)
        {
            return NotFound();
        }

        await _groceryList.DeleteAsync(item);
        return NoContent();
    }
}