using Domain.Entities;
using Domain.Exceptions.GroceryList;
using Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Services;

public class GroceryListItemService : IGroceryListItemService
{
    private readonly IApiDbContext _dbContext;
    private readonly ILogger<GroceryListItemService> _logger;

    public GroceryListItemService(IApiDbContext dbContext, ILogger<GroceryListItemService> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }
    
    public async Task<GroceryListItemEntity> GetByIdAsync(int groceryListItem)
    {
        var item = await _dbContext.GroceryListItem.FindAsync(groceryListItem);
        if (item == null)
        {
            throw new ItemDoesNotExistException();
        }

        return item;
    }
    
    public async Task CreateAsync(GroceryListItemEntity item)
    {
        if (!item.ItemName.Any(Char.IsLetter))
        {
            throw new ArgumentException("Must contain a letter");
        }
        if (await GrocerylistExists(item.GroceryListId))
        {
            _dbContext.GroceryListItem.Add(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new GroceryListDoesNotExistException();
        }
    }
    
    public async Task UpdateAsync(GroceryListItemEntity item)
    {
        // Check the provided item id exists
        var checkItem = await GetByIdAsync(item.GroceryListItemId);
        
        // check grocerylist exists 
        if (!await GrocerylistExists(item.GroceryListId))
        {
            throw new GroceryListDoesNotExistException();
        }

        // update values
        checkItem.ItemName = item.ItemName;
        checkItem.ItemAmount = item.ItemAmount;
        
        _dbContext.GroceryListItem.Update(checkItem);
        await _dbContext.SaveChangesAsync();
    }
    
    
    public async Task DeleteAsync(int itemId)
    {
        // Check the provided item id exists
        var item = await _dbContext.GroceryListItem.FindAsync(itemId);
        if (item == null)
        {
            throw new ItemDoesNotExistException();
        }
        _dbContext.GroceryListItem.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task<bool> GrocerylistExists(int grocerylistId)
    {
        var grocerylist = await _dbContext.GroceryList.FindAsync(grocerylistId);
        if (grocerylist == null)
        {
            return false;
        }

        return true;
    }
}