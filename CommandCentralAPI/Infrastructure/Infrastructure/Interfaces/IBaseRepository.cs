namespace Infrastructure.Interfaces;

public interface IBaseRepository<TEntity>
{
    Task<TEntity> GetByIdAsync(int itemId, int householdId);
    Task<TEntity> CreateAsync(TEntity paramItem, int householdId);
    Task UpdateAsync(TEntity item, int householdId);
    Task DeleteAsync(int itemId, int householdId);

    Task CheckMealId(int mealId, int householdId);
}