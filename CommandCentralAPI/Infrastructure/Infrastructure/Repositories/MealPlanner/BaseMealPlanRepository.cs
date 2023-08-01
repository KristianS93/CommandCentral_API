using Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure.Repositories.MealPlanner;

public abstract class BaseMealPlanRepository<TService, TEntity> 
    where TEntity : class
    where TService : IBaseService<TEntity>
{
    private readonly DbSet<TEntity> _dbSet;
    protected readonly IApiDbContext DbContext;
    protected readonly TService Service;

    protected BaseMealPlanRepository(TService service, DbSet<TEntity> dbSet, IApiDbContext dbContext)
    {
        Service = service;
        _dbSet = dbSet;
        DbContext = dbContext;
    }


    public abstract Task<TEntity> GetByIdAsync(int itemId, int householdId);

    public async Task<TEntity> CreateAsync(TEntity paramItem, int householdId)
    {
        var createItem = Service.Create(paramItem);
        await SpecificCreateAsync(createItem, householdId);
        _dbSet.Add(createItem);
        await DbContext.SaveChangesAsync();
        return createItem;
    }
    
    public async Task UpdateAsync(TEntity item, int householdId, int mealId)
    {
        await CheckMealId(mealId, householdId);
        var changeItem = Service.Update(item);
        _dbSet.Update(changeItem);
        await DbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int itemId, int householdId)
    {
        var deleteItem = await GetByIdAsync(itemId, householdId);
        deleteItem = Service.Delete(deleteItem);
        _dbSet.Remove(deleteItem);
        await DbContext.SaveChangesAsync();
    }
    
    public async Task CheckMealId(int mealId, int householdId)
    {
        // get meal
        var mealHousehold = await DbContext.Meal.FindAsync(mealId);
        
        ArgumentNullException.ThrowIfNull(mealHousehold);
        
        if (mealHousehold.HouseholdId != householdId)
        {
            throw new ArgumentException("Meal does not belong to household");
        }
    }
    protected abstract Task SpecificCreateAsync(TEntity createItem, int compareId);
}