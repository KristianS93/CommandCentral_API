using System.Security.Claims;
using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;
using Domain.Models.Authentication;
using Infrastructure.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Interfaces.MealPlanner;
using Infrastructure.Repositories.MealPlanner;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[CustomAuthorize(Permission.Member)]
public class IngredientController : ControllerBase
{
    private readonly IIngredientRepository _ingredientRepository;
    private readonly ILogger<IngredientController> _logger;
    private readonly int _householdId;

    public IngredientController(IIngredientRepository ingredientRepository, ILogger<IngredientController> logger, IClaimAuthorizationService claimAuthorization)
    {
        _ingredientRepository = ingredientRepository;
        _logger = logger;
        _householdId = claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
    }

    [HttpGet]
    [Route("[controller]/{Id}")]
    public async Task<ActionResult<IngredientEntity>> GetIngredient(int Id)
    {
        try
        {
            return Ok(await _ingredientRepository.GetByIdAsync(Id, _householdId));
        }
        // this exception handling should be changed to it follows the correct
        // pattern for error responses..
        catch (Exception e)
        {
            return NotFound(e.InnerException);
        }
    }

    [HttpPost]
    [Route("[controller]")]
    public async Task<ActionResult<IngredientEntity>> CreateIngredient(IngredientEntity ingredient)
    {
        try
        {
            return Created($"{ControllerContext.ActionDescriptor.ControllerName}/", await _ingredientRepository.CreateAsync(ingredient, _householdId));
        }
        catch (Exception e)
        {
            return NotFound(e.InnerException);
        }
    }

    [HttpPut]
    [Route("[controller]")]
    public async Task<IActionResult> UpdateIngredient(IngredientEntity ingredient)
    {
        try
        {
            await _ingredientRepository.UpdateAsync(ingredient, _householdId);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.InnerException);
        }
    }

    [HttpDelete]
    [Route("[controller]/{Id}")]
    public async Task<IActionResult> DeleteIngredient(int Id)
    {
        try
        {
            await _ingredientRepository.DeleteAsync(Id, _householdId);
            return Ok();
        }
        catch (Exception e)
        {
            return NotFound(e.InnerException);
        }
    }
}