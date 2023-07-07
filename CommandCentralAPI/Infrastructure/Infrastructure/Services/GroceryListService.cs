using Domain.Entities;
using Domain.Exceptions;
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

    public async Task CreateAsync(int householdId)
    {
        // Check household exsists
        var household = await _dbContext.Household.FindAsync(householdId);
        if (household == null)
        {
            throw new HouseholdException("Household does not exist");
        }
        // Check whether the household already have a grocerylist
        var item = await GetAsyncByHousehold(householdId);
        if (item != null)
        {
            throw new ArgumentException("Grocerylist already exist for this household!");
        }
        
        
        // Create new instance and update database 
        var groceryList = new GroceryListEntity { HouseholdId = householdId };
        _dbContext.GroceryList.Add(groceryList);
        await _dbContext.SaveChangesAsync();
    }
    
}