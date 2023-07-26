using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection service, ConfigurationManager conf)
    {
        var connectionString =
            Environment.GetEnvironmentVariable("POSTGRESQLCONNSTR_AZURE_POSTGRESQL_CONNECTIONSTRING");
        if (String.IsNullOrEmpty(connectionString))
        {
            // Local Development
            connectionString = conf.GetConnectionString("Postgres");
        }
        service.AddDbContext<IApiDbContext, ApiDbContext>(options =>
            options.UseNpgsql(connectionString));
        return service;
    }
}