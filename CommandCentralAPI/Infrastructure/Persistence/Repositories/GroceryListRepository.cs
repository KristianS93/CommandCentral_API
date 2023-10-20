using Application.Contracts.GroceryList;
using Domain.Entities.GroceryList;
using Domain.Entities.Household;
using Microsoft.EntityFrameworkCore;
using Persistence.DatabaseContext;

namespace Persistence.Repositories;

public class GroceryListRepository : GenericRepository<GroceryListEntity>, IGroceryListRepository 
{
    private readonly ApiDbContext _dbContext;

    public GroceryListRepository(ApiDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GroceryListEntity?> GetGroceryListByHouseholdIdAsync(int householdId)
    {
        return await _dbContext.Set<GroceryListEntity>().AsNoTracking()
            .FirstOrDefaultAsync(e => e.HouseholdId == householdId);
    }

    public async Task<bool> IsOwnerOfGroceryList(int groceryListId, int householdId)
    {
        return await _dbContext.GroceryList.AnyAsync(e => e.Id == groceryListId && e.HouseholdId == householdId);
    }
}