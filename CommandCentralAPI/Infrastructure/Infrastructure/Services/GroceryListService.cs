using Domain.Entities;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Services;

public class GroceryListService : IGroceryListService
{
    private readonly IApiDbContext _dbContext;
    private readonly ILogger<GroceryListService> _logger;

    public GroceryListService(IApiDbContext dbContext, ILogger<GroceryListService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<GroceryListEntity> GetAsyncByHousehold(int householdId)
    {
        return await _dbContext.GroceryList.FirstOrDefaultAsync(h => h.HouseholdId == householdId);
    }
    public async Task DeleteAsync(GroceryListEntity item)
    {
        _dbContext.GroceryList.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
}