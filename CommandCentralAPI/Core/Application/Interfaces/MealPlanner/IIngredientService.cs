using Application.Services.MealPlanner;
using Domain.Entities.MealPlanner;

namespace Application.Interfaces.MealPlanner;

public interface IIngredientService<TService> : IBaseService<TService> where TService : IngredientEntity
{
    
}