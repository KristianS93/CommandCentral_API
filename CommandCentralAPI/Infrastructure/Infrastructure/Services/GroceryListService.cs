using Domain.Entities;
using Domain.Entities.GroceryList;
using Domain.Exceptions;
using Domain.Exceptions.GroceryList;
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
    
    public async Task<GroceryListEntity> GetAsyncByHouseholdIdAsync(int householdId)
    {
        var item = await _dbContext.GroceryList.FirstOrDefaultAsync(h => h.HouseholdId == householdId);
        if (item == null)
        {
            throw new GroceryListDoesNotExistException();
        }
        
        // Get grocerylist items
        var groceryListitems = await _dbContext.GroceryListItem.Where(e => e.GroceryListId == item.Id).ToListAsync();
        item.GroceryListItems = groceryListitems;
        return item;
    }

    public async Task DeleteAsync(int household_id)
    {
        var item = await GetAsyncByHouseholdIdAsync(household_id);
        if (item == null)
        {
            throw new GroceryListDoesNotExistException();
        }
        _dbContext.GroceryList.Remove(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(int householdId)
    {
        // Check household exsists
        var household = await _dbContext.Household.FindAsync(householdId);
        if (household == null)
        {
            throw new HouseholdDoesNotExistException("Household does not exist");
        }
        
        // Check whether the household already have a grocerylist
        try
        {
            var item = await GetAsyncByHouseholdIdAsync(householdId);
            if (item != null)
            {
                throw new GroceryListDuplicateException("Grocerylist already exist for this household!");
            }
        }
        catch(GroceryListDoesNotExistException)
        {
            // OK, the grocerylist does not exist
            // Create new instance and update database 
            var groceryList = new GroceryListEntity { HouseholdId = householdId };
            _dbContext.GroceryList.Add(groceryList);
            await _dbContext.SaveChangesAsync();
        }
    }
    
}