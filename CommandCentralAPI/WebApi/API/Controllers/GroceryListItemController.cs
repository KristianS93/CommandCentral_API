using System.Security.Claims;
using Application.Contracts.Identity;
using Application.Features.GroceryListItem.Queries.GetAllGroceryListItems;
using Application.Features.GroceryListItem.Shared;
using Identity.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
[CustomAuthorize(Permission.Member)]
public class GroceryListItemController : ControllerBase
{
    private readonly IMediator _mediatr;
    private readonly IClaimAuthorizationService _claimAuthorizationService;

    public GroceryListItemController(IMediator mediatr, IClaimAuthorizationService claimAuthorizationService)
    {
        _mediatr = mediatr;
        _claimAuthorizationService = claimAuthorizationService;
    }
    [HttpGet("/Items")] // this route is shit
    [CustomAuthorize(Permission.Member)]
    public async Task<List<GetGroceryListItemDto>> GetGroceryListItems()
    {
        var groceryListId =
            _claimAuthorizationService.GetIntegerClaimId(User.FindFirstValue(Claims.GroceryList.ToString())!);

        var data = await _mediatr.Send(new GetAllGroceryListItemsQuery(groceryListId));

        return data;
    }
}