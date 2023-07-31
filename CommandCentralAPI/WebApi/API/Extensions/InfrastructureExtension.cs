using Infrastructure.Interfaces;
using Infrastructure.Interfaces.MealPlanner;
using Infrastructure.Repositories.MealPlanner;
using Infrastructure.Services;

namespace API.Extensions;

public static class InfrastructureExtension
{
    /// <summary>
    /// Add services
    /// </summary>
    /// <param name="service"></param>
    /// <returns>IServiceCollection</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection service)
    {
        service.AddScoped<ITodoService, TodoService>();
        service.AddScoped<IHouseholdService, HouseholdService>();
        service.AddScoped<IGroceryListService, GroceryListService>();
        service.AddScoped<IGroceryListItemService, GroceryListItemService>();
        service.AddScoped<IIngredientRepository, IngredientRepository>();
        return service;
    }
}