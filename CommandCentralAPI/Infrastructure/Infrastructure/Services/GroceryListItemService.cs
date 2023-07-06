using Domain.Entities;
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="groceryListId"></param>
    /// <returns></returns>
    /// <exception cref="ArgumentException"></exception>
    public async Task<List<GroceryListItemEntity>> GetAllAsync(int groceryListId)
    {
        if (await GrocerylistExists(groceryListId))
        {
            return await _dbContext.GroceryListItem.Where(e => e.GroceryListId == groceryListId).ToListAsync();
        }
        else
        {
            throw new ArgumentException("Grocerylist doesn't exist");
        }
    }

    public async Task<GroceryListItemEntity> GetByIdAsync(int groceryListItem)
    {
        return await _dbContext.GroceryListItem.FindAsync(groceryListItem);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task UpdateAsync(GroceryListItemEntity item)
    {
        if (await GrocerylistExists(item.GroceryListId))
        {
            _dbContext.GroceryListItem.Update(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Grocerylist doesn't exist");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="item"></param>
    /// <exception cref="ArgumentNullException"></exception>
    public async Task CreateAsync(GroceryListItemEntity item)
    {
        if (await GrocerylistExists(item.GroceryListId))
        {
            item.GroceryList = await _dbContext.GroceryList.FindAsync(item.GroceryListId);
            _dbContext.GroceryListItem.Add(item);
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            throw new ArgumentNullException("Grocerylist doesn't exist");
        }
    }
    
    public async Task DeleteAsync(int itemId)
    {
        var item = await _dbContext.GroceryListItem.FindAsync(itemId);
        if (item == null)
        {
            throw new ArgumentNullException($"Item with id {itemId} does not exist.");
        }
        _dbContext.GroceryListItem.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task<bool> GrocerylistExists(int grocerylistId)
    {
        var grocerylist = await _dbContext.GroceryList.FindAsync(grocerylistId);
        if (grocerylist is null)
        {
            return false;
        }

        return true;
    }
}