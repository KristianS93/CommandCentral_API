using System.Security.Authentication;
using Infrastructure.Authentication.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure.Authentication;

public class ClaimAuthorizationService : IClaimAuthorizationService
{
    private readonly IApiDbContext _dbContext;

    public ClaimAuthorizationService(IApiDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void ConfirmHouseholdClaim(string claim, int householdId)
    {
        if (Convert.ToInt32(claim) != householdId)
        {
            throw new AuthenticationException();
        }
    }

    public void ConfirmGroceryListClaim(string claim, int groceryListId)
    {
        if (Convert.ToInt32(claim) != groceryListId)
        {
            throw new AuthenticationException();
        }
    }

    public void ConfirmItemOnList(int groceryListId, int groceryListItemId)
    {
        // Get list based on groceryListId
        var items = _dbContext.GroceryListItem.Where(k => k.GroceryListId == groceryListId).ToList();
        if (items.All(k => k.GroceryListItemId != groceryListItemId))
        {
            throw new AuthenticationException();
        }
    }
}