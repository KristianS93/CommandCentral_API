using Domain.Entities;
using Domain.Entities.Authentication;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Persistence.Data;

public class ApiDbContext : DbContext, IApiDbContext
{
    private readonly ILogger<ApiDbContext> _logger;

    public ApiDbContext(DbContextOptions<ApiDbContext> options, ILogger<ApiDbContext> logger) : base(options)
    {
        _logger = logger;
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<GroceryListEntity>()
            .HasIndex(key => key.HouseholdId)
            .IsUnique();

        modelBuilder.Entity<GroceryListEntity>()
            .HasOne<HouseholdEntity>()
            .WithOne()
            .HasForeignKey<GroceryListEntity>("household")
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<GroceryListItemEntity>()
            .HasOne<GroceryListEntity>()
            .WithMany()
            .HasForeignKey("grocerylist")
            .OnDelete(DeleteBehavior.Cascade);
    }

    public DbSet<MemberEntity> Members { get; set; }
    public DbSet<HouseholdEntity> Household { get; set; }
    public DbSet<GroceryListEntity> GroceryList { get; set; }
    public DbSet<GroceryListItemEntity> GroceryListItem { get; set; }
    public DbSet<TodoItem> TodoItems { get; set; }
    public async Task<int> SaveChangesAsync()
    {
        return await base.SaveChangesAsync();
    }
}