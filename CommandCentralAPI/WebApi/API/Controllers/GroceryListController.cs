using Domain.Entities;
using Domain.Exceptions;
using Domain.Models.ErrorResponses;
using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[Route("[controller]")]
public class GroceryListController : ControllerBase
{
    private readonly ILogger<GroceryListController> _logger;
    private readonly IGroceryListService _groceryList;

    public GroceryListController(ILogger<GroceryListController> logger, IGroceryListService groceryList, IHouseholdService householdService)
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
    
    // skal ændres 
    [HttpPost("{householdId}")]
    public async Task<ActionResult> CreateGroceryList(int householdId)
    {
        try
        {
            await _groceryList.CreateAsync(householdId);
        }
        catch (ArgumentException e)
        {
            // needs own error response
            return Conflict(e.Message);
        }
        catch (HouseholdException e)
        {
            var error = new HouseHoldErrors(householdId,
                $"{ControllerContext.ActionDescriptor.ControllerName}/{householdId}").HouseholdDoesNotExist();
            return NotFound(error);
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