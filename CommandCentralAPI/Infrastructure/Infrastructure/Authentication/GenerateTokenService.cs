using Domain.Exceptions;
using Infrastructure.Authentication.Interfaces;
using Infrastructure.Interfaces;
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

    public async Task<string> GenerateToken(int householdId)
    {
        // Get household
        var household = await _dbContext.Household.FindAsync(householdId);
        if (household == null)
        {
            throw new HouseholdDoesNotExistException();
        }
        
        // Generate JWT
        string token = _jwtProvider.Generate(household);
        
        // Return JWT
        return token;
    }
}