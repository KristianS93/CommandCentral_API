using Application.Contracts.Household;
using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DatabaseContext;
using Persistence.Repositories;

namespace Persistence;

public static class PersistenceServiceRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        // azure specific
        var connectionString =
            Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");
        
        // Local development
        if (String.IsNullOrEmpty(connectionString))
        {
            connectionString = configuration.GetConnectionString("Postgres");
        }
        services.AddDbContext<ApiDbContext>(options =>
        {
            options.UseNpgsql(connectionString).UseSnakeCaseNamingConvention();
        });
        Console.WriteLine(connectionString);
        
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IHouseholdRepository, HouseholdRepository>();
        return services;
    }
}