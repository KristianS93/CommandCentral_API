using Microsoft.EntityFrameworkCore;
using Persistence.Data;

namespace API.Extensions;

public static class PersistenceExtension
{
    public static IServiceCollection AddPersistence(this IServiceCollection service, ConfigurationManager conf)
    {
        service.AddDbContext<IApiDbContext, ApiDbContext>(options =>
            options.UseNpgsql(conf.GetConnectionString("Postgres")));
        return service;
    }
}