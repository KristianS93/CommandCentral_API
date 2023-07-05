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

    public async Task<List<GroceryListItemEntity>> GetAllAsync(int groceryListId)
    {
        return await _dbContext.GroceryListItem.Where(e => e.GroceryListId == groceryListId).ToListAsync();
    }

    public async Task<GroceryListItemEntity> GetByIdAsync(int groceryListItem)
    {
        return await _dbContext.GroceryListItem.FindAsync(groceryListItem);
    }

    public async Task UpdateAsync(GroceryListItemEntity item)
    {
        _dbContext.GroceryListItem.Update(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task CreateAsync(GroceryListItemEntity item)
    {
        _dbContext.GroceryListItem.Add(item);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(GroceryListItemEntity item)
    {
        _dbContext.GroceryListItem.Remove(item);
        await _dbContext.SaveChangesAsync();
    }
}