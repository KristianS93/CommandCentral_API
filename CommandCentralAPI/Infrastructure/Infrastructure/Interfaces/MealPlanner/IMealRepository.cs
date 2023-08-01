using Application.Interfaces;
using Domain.Entities.MealPlanner;

namespace Infrastructure.Interfaces.MealPlanner;

public interface IMealRepository : IBaseRepository<MealEntity>
{
    Task<IEnumerable<MealEntity>> GetAllMealsAsync(int household);
}