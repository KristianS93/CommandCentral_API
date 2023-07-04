using Infrastructure.Interfaces;
using Infrastructure.Services;

namespace API;

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

        return service;
    }
}