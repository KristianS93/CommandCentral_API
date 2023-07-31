using Application.Interfaces.MealPlanner;
using Application.Services.MealPlanner;
using Domain.Entities.MealPlanner;

namespace API.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection service)
    {
        service.AddScoped<IIngredientService<IngredientEntity>, IngredientService>();

        return service;
    }
}