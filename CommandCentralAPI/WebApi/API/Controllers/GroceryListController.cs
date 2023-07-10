using Domain.Entities;
using Domain.Exceptions;
using Domain.Exceptions.GroceryList;
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

    public GroceryListController(ILogger<GroceryListController> logger, IGroceryListService groceryList)
    {
        _logger = logger;
        _groceryList = groceryList;
    }

    [HttpGet("{household_id}")]
    public async Task<ActionResult<GroceryListEntity>> GetGroceryList(int household_id)
    {
        try
        {
            return Ok(await _groceryList.GetAsyncByHouseholdId(household_id));
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(household_id,ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }
    
    [HttpPost("{household_id}")]
    public async Task<ActionResult> CreateGroceryList(int household_id)
    {
        try
        {
            await _groceryList.CreateAsync(household_id);
            return Created($"{ControllerContext.ActionDescriptor.ControllerName}/{household_id}", null);
        }
        catch (GroceryListDuplicateException e)
        {
            var error = new GroceryListErrors().GroceryListDuplicateHousehold(household_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return Conflict(error);
        }
        catch (HouseholdDoesNotExistException e)
        {
            var error = new HouseHoldErrors().HouseholdDoesNotExist(household_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }

    [HttpDelete("{household_id}")]
    public async Task<IActionResult> DeleteGroceryList(int household_id)
    {
        try
        {
            await _groceryList.DeleteAsync(household_id);
            return NoContent();
        }
        catch (GroceryListDoesNotExistException)
        {
            var error = new GroceryListErrors().GroceryListDoesNotExist(household_id,
                ControllerContext.ActionDescriptor.ControllerName);
            return NotFound(error);
        }
    }
}