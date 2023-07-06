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
    private readonly IHouseholdService _householdService;

    public GroceryListController(ILogger<GroceryListController> logger, IGroceryListService groceryList, IHouseholdService householdService)
    {
        _logger = logger;
        _groceryList = groceryList;
        _householdService = householdService;
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
    
    // skal ændres 
    [HttpPost("{id}")]
    public async Task<ActionResult> CreateGroceryList(int id)
    {
        var household = await _householdService.GetByIdAsync(id);
        if (household == null)
        {
            return NotFound();
        }
        try
        {
            await _groceryList.CreateAsync(id);
        }
        catch (ArgumentException e)
        {
            return Conflict(e.Message);
        }

        return Ok();
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