// using System.Security.Claims;
// using Application.Interfaces.MealPlanner;
// using Domain.Entities.MealPlanner;
// using Domain.Models.Authentication;
// using Infrastructure.Authentication;
// using Infrastructure.Authentication.Interfaces;
// using Infrastructure.Interfaces.MealPlanner;
// using Infrastructure.Repositories.MealPlanner;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API.Controllers;
//
// [ApiController]
// [CustomAuthorize(Permission.Member)]
// public class IngredientController : ControllerBase
// {
//     private readonly IIngredientRepository _ingredientRepository;
//     private readonly ILogger<IngredientController> _logger;
//     private readonly IClaimAuthorizationService _claimAuthorization;
//
//     public IngredientController(IIngredientRepository ingredientRepository, ILogger<IngredientController> logger, IClaimAuthorizationService claimAuthorization)
//     {
//         _ingredientRepository = ingredientRepository;
//         _logger = logger;
//         _claimAuthorization = claimAuthorization;
//     }
//
//     [HttpGet]
//     [Route("[controller]/{Id}")]
//     public async Task<ActionResult<IngredientEntity>> GetIngredient(int Id)
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             return Ok(await _ingredientRepository.GetByIdAsync(Id, householdId));
//         }
//         // this exception handling should be changed to it follows the correct
//         // pattern for error responses..
//         catch (Exception e)
//         {
//             return NotFound(e.InnerException);
//         }
//     }
//
//     [HttpPost]
//     [Route("[controller]")]
//     public async Task<ActionResult<IngredientEntity>> CreateIngredient(IngredientEntity ingredient)
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             return Created($"{ControllerContext.ActionDescriptor.ControllerName}/", await _ingredientRepository.CreateAsync(ingredient, householdId));
//         }
//         catch (Exception e)
//         {
//             return NotFound(e.InnerException);
//         }
//     }
//
//     [HttpPut]
//     [Route("[controller]")]
//     public async Task<IActionResult> UpdateIngredient(IngredientEntity ingredient)
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             await _ingredientRepository.UpdateAsync(ingredient, householdId);
//             return Ok();
//         }
//         catch (Exception e)
//         {
//             return NotFound(e.InnerException);
//         }
//     }
//
//     [HttpDelete]
//     [Route("[controller]/{Id}")]
//     public async Task<IActionResult> DeleteIngredient(int Id)
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             await _ingredientRepository.DeleteAsync(Id, householdId);
//             return Ok();
//         }
//         catch (Exception e)
//         {
//             return NotFound(e.InnerException);
//         }
//     }
// }