using Domain.Entities.MealPlanner;
using Infrastructure.Interfaces.MealPlanner;

namespace Infrastructure.Repositories.MealPlanner;

public class MealRepository : IMealRepository
{
    public async Task<MealEntity> GetByIdAsync(int itemId, int householdId)
    {
        throw new NotImplementedException();
    }

    public async Task<MealEntity> CreateAsync(MealEntity item)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(MealEntity item, int householdId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int itemId, int householdId)
    {
        throw new NotImplementedException();
    }
}