using Application.Contracts.Persistence;
using Domain.Entities.MealPlanner;

namespace Application.Contracts.MealPlanner;

public interface IIngredientRepository : IGenericRepository<IngredientEntity>
{
    
}