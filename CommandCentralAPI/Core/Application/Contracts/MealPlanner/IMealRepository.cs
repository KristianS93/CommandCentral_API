using Application.Contracts.Persistence;
using Domain.Entities.MealPlanner;

namespace Application.Contracts.MealPlanner;

public interface IMealRepository : IGenericRepository<MealEntity>
{
    Task<IEnumerable<MealEntity>> GetAllMealsAsync(int household);
}