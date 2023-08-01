using Application.Interfaces.MealPlanner;
using Domain.Entities.MealPlanner;
using Infrastructure.Interfaces;
using Infrastructure.Interfaces.MealPlanner;
using Infrastructure.Repositories.MealPlanner;
using Infrastructure.Services;
using Microsoft.EntityFrameworkCore;

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
        service.AddScoped<IMealRepository, MealRepository>();
        return service;
    }
}