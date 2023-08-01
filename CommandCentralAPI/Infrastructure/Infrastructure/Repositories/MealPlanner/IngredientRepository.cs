using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;
using Infrastructure.Interfaces.MealPlanner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Persistence.Data;

namespace Infrastructure.Repositories.MealPlanner;

public class IngredientRepository : IIngredientRepository
{
    private readonly IApiDbContext _dbContext;
    private readonly IIngredientService _ingredientService;
    
    public IngredientRepository(IApiDbContext dbContext, IIngredientService ingredientService)
    {
        _dbContext = dbContext;
        _ingredientService = ingredientService;
    }

    public async Task<IngredientEntity> GetByIdAsync(int itemId, int householdId)
    {
        var ingredient = await _dbContext.Ingredient.FindAsync(itemId);
        await CheckMealId(ingredient.MealId, householdId);
        ingredient = _ingredientService.GetByItem(ingredient);
        return ingredient;
    }
    
    public async Task<IngredientEntity> CreateAsync(IngredientEntity item, int householdId)
    {
        // check meal id exists
        var meal = await _dbContext.Meal.FindAsync(item.MealId);
        ArgumentNullException.ThrowIfNull(meal);
        
        // make sure im the owner of the meal
        await CheckMealId(meal.Id, householdId);
        
        // create
        var ingredient = _ingredientService.Create(item);
        _dbContext.Ingredient.Add(ingredient);
        await _dbContext.SaveChangesAsync();
        return ingredient;
    }
    //
    public async Task UpdateAsync(IngredientEntity item, int householdId)
    {
        var ingredient = _ingredientService.Update(item);
        await CheckMealId(ingredient.MealId, householdId);
        _dbContext.Ingredient.Update(ingredient);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteAsync(int itemId, int householdId)
    {
        var ingredient = await GetByIdAsync(itemId, householdId);
        await CheckMealId(ingredient.MealId, householdId);
        ingredient = _ingredientService.Delete(ingredient);
        _dbContext.Ingredient.Remove(ingredient);
        await _dbContext.SaveChangesAsync();
    }
    //
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