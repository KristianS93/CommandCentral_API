using CommandCentralAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CommandCentralAPI.Data;

public class DatabaseContext : DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }
    public DbSet<Products> products { get; set; }
}