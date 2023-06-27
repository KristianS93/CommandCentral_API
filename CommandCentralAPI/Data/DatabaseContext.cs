using CommandCentralAPI.dbmodels;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace CommandCentralAPI.Data;

public class DatabaseContext : DbContext
{
    private readonly ILogger<DatabaseContext> _logger;
    public DatabaseContext(DbContextOptions<DatabaseContext> options, ILogger<DatabaseContext> logger) : base(options)
    {
        _logger = logger;
        if (Database.CanConnect())
        {
            _logger.LogInformation("Connection established");
        }
        else
        {
            _logger.LogCritical("Error connecting to database");
            throw new NpgsqlException("Could not connect to database");
        }

        }
    public DbSet<DbHousehold> household { get; set; }
    public DbSet<DbGroceryList> grocery_list { get; set; }
    public DbSet<DbGroceryListItem> grocery_list_item { get; set; }
}
