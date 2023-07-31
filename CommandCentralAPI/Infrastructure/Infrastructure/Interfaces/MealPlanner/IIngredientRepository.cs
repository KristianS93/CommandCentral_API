using Domain.Entities.MealPlanner;

namespace Infrastructure.Interfaces.MealPlanner;

public interface IIngredientRepository : IBaseRepository<IngredientEntity>
{
    Task CheckMealId(int mealId, int householdId);
}