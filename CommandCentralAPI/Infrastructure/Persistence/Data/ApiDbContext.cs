using Domain.Entities;
using Domain.Entities.Authentication;
using Domain.Entities.MealPlanner;
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
        // GroceryList entity behavior
        modelBuilder.Entity<GroceryListEntity>()
            .HasIndex(key => key.HouseholdId)
            .IsUnique();
        
        modelBuilder.Entity<GroceryListEntity>()
            .HasOne<HouseholdEntity>()
            .WithOne()
            .HasForeignKey<GroceryListEntity>("household")
            .OnDelete(DeleteBehavior.Cascade);
        
        // Grocerylist item entity
        modelBuilder.Entity<GroceryListItemEntity>()
            .HasOne<GroceryListEntity>()
            .WithMany()
            .HasForeignKey("grocerylist")
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WeekPlanEntity>()
            .HasOne<HouseholdEntity>()
            .WithMany()
            .HasForeignKey("household")
            .OnDelete(DeleteBehavior.Cascade);
        
        // time stamp behaviors
        ConfigureTimestamps<BaseEntity>(modelBuilder);
    }

    private void ConfigureTimestamps<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
    {
        modelBuilder.Entity<TEntity>()
            .Property(e => e.CreatedAt)
            .HasColumnType("timestamp");
        
        modelBuilder.Entity<TEntity>()
            .Property(e => e.LastModified)
            .HasColumnType("timestamp");
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