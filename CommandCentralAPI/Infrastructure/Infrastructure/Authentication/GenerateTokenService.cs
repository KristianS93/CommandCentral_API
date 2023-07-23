using Domain.Exceptions;
using Domain.Models.Authentication;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure.Authentication;

public class GenerateTokenService : IGenerateTokenService
{
    private readonly IApiDbContext _dbContext;
    private readonly IJwtProvider _jwtProvider;

    public GenerateTokenService(IJwtProvider jwtProvider, IApiDbContext dbContext)
    {
        _jwtProvider = jwtProvider;
        _dbContext = dbContext;
    }

    public async Task<string> GenerateToken(int householdId, Permission permission)
    {
        // Get household
        var household = await _dbContext.Household.FindAsync(householdId);
        if (household == null)
        {
            throw new HouseholdDoesNotExistException();
        }
        
        // If grocerylist exist add this to the claim 
        var groceryList = await _dbContext.GroceryList.Where(hId => hId.HouseholdId == householdId).FirstOrDefaultAsync();

        // Generate JWT
        string token = _jwtProvider.Generate(household, groceryList, permission);
        
        // Return JWT
        return token;
    }
}