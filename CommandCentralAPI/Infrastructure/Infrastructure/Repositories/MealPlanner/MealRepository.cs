using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;
using Infrastructure.Interfaces.MealPlanner;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace Infrastructure.Repositories.MealPlanner;

public class MealRepository : IMealRepository
{
    private readonly IApiDbContext _dbContext;
    private readonly IMealService _service;
    public MealRepository(IApiDbContext dbContext, IMealService service)
    {
        _dbContext = dbContext;
        _service = service;
    }

    public async Task<IEnumerable<MealEntity>> GetAllMealsAsync(int householdId)
    {
        var meals = await _dbContext.Meal.Where(k => k.HouseholdId == householdId).ToListAsync();
        return meals;
    }

    public async Task<MealEntity> GetByIdAsync(int itemId, int householdId)
    {
        var meal = await _dbContext.Meal.FindAsync(itemId);
        await CheckMealId(meal.Id, householdId);
        meal = _service.GetByItem(meal);
        return meal;
    }

    public async Task<MealEntity> CreateAsync(MealEntity paramItem, int householdId)
    {
        ArgumentNullException.ThrowIfNull(paramItem);
        
        var meal = _service.Create(paramItem);
        _dbContext.Meal.Add(meal);
        await _dbContext.SaveChangesAsync();
        return meal;
    }

    public async Task UpdateAsync(MealEntity item, int householdId)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(int itemId, int householdId)
    {
        throw new NotImplementedException();
    }

    public async Task CheckMealId(int mealId, int householdId)
    {
        // get meal
        var mealHousehold = await _dbContext.Meal.FindAsync(mealId);
        ArgumentNullException.ThrowIfNull(mealHousehold);

        if (mealHousehold.HouseholdId != householdId)
        {
            throw new ArgumentException("Meal does not belong to household");
        }

    }
}