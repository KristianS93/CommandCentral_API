// using System.Security.Authentication;
// using System.Security.Claims;
// using Domain.Entities;
// using Domain.Entities.GroceryList;
// using Domain.Exceptions;
// using Domain.Exceptions.GroceryList;
// using Domain.Models.Authentication;
// using Domain.Models.ErrorResponses;
// using Infrastructure.Authentication;
// using Infrastructure.Authentication.Interfaces;
// using Infrastructure.Interfaces;
// using Microsoft.AspNetCore.Mvc;
//
// namespace API.Controllers;
//
// [ApiController]
// [Route("[controller]")]
// public class GroceryListController : ControllerBase
// {
//     private readonly ILogger<GroceryListController> _logger;
//     private readonly IGroceryListService _groceryList;
//     private readonly IClaimAuthorizationService _claimAuthorization;
//     
//
//     public GroceryListController(ILogger<GroceryListController> logger, IGroceryListService groceryList, IClaimAuthorizationService claimAuthorization)
//     {
//         _logger = logger;
//         _groceryList = groceryList;
//         _claimAuthorization = claimAuthorization;
//     }
//
//     [HttpGet]
//     [CustomAuthorize(Permission.Member)]
//     public async Task<ActionResult<GroceryListEntity>> GetGroceryList()
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             return Ok(await _groceryList.GetAsyncByHouseholdIdAsync(householdId));
//         }
//         catch (GroceryListDoesNotExistException)
//         {
//             var error = new GroceryListErrors().GroceryListDoesNotExist(ControllerContext.ActionDescriptor.ControllerName);
//             return NotFound(error);
//         }
//         catch (AuthenticationException)
//         {
//             var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
//             return Unauthorized(error);
//         }
//     }
//     
//     [HttpPost]
//     [CustomAuthorize(Permission.HouseholdAdmin)]
//     public async Task<ActionResult> CreateGroceryList()
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             await _groceryList.CreateAsync(householdId);
//             return Created($"{ControllerContext.ActionDescriptor.ControllerName}/", null);
//         }
//         catch (GroceryListDuplicateException e)
//         {
//             var error = new GroceryListErrors().GroceryListDuplicateHousehold(
//                 ControllerContext.ActionDescriptor.ControllerName);
//             return Conflict(error);
//         }
//         catch (HouseholdDoesNotExistException e)
//         {
//             var error = new HouseHoldErrors().HouseholdDoesNotExist(
//                 ControllerContext.ActionDescriptor.ControllerName);
//             return NotFound(error);
//         }
//         catch (AuthenticationException)
//         {
//             var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
//             return Unauthorized(error);
//         }
//     }
//
//     [HttpDelete]
//     [CustomAuthorize(Permission.HouseholdAdmin)]
//     public async Task<IActionResult> DeleteGroceryList()
//     {
//         try
//         {
//             var householdId = _claimAuthorization.GetIntegerClaimId(User.FindFirstValue(Claims.Household.ToString())!);
//             await _groceryList.DeleteAsync(householdId);
//             return NoContent();
//         }
//         catch (GroceryListDoesNotExistException)
//         {
//             var error = new GroceryListErrors().GroceryListDoesNotExist(
//                 ControllerContext.ActionDescriptor.ControllerName);
//             return NotFound(error);
//         }
//         catch (AuthenticationException)
//         {
//             var error = new AuthenticationErrors().AccessDenied(ControllerContext.ActionDescriptor.ControllerName);
//             return Unauthorized(error);
//         }
//     }
// }