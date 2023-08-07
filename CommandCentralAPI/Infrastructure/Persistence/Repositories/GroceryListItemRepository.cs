using Application.Contracts.GroceryList;
using Domain.Entities.GroceryList;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class GroceryListItemRepository : GenericRepository<GroceryListItemEntity>, IGroceryListItemRepository
{
    private readonly ApiDbContext _dbContext;

    public GroceryListItemRepository(ApiDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<List<GroceryListItemEntity>> GetGroceryListItemAsync(int groceryListId)
    {
        return await _dbContext.Set<GroceryListItemEntity>().AsNoTracking().Where(e => e.GroceryListId == groceryListId).ToListAsync();
    }
}