using Domain.Common;
using Domain.Entities.GroceryList;
using Domain.Entities.Household;
using Domain.Entities.MealPlanner;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DatabaseContext;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        // modelBuilder.Entity<BaseEntity>()
        //     .ToTable("BaseEntity")
        //     .HasKey(e => e.Id)
        //     .HasName("pk_base_entity");
        //
        // modelBuilder.Entity<HouseholdEntity>()
        //     .ToTable("household") // Set the table name for HouseholdEntity
        //     .HasKey(e => e.Id)
        //     .HasName("pk_household");
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApiDbContext).Assembly);
        // GroceryList entity behavior
        // modelBuilder.Entity<GroceryListEntity>()
        //     .HasIndex(key => key.HouseholdId)
        //     .IsUnique();
        //
        // modelBuilder.Entity<GroceryListEntity>()
        //     .HasOne<HouseholdEntity>()
        //     .WithOne()
        //     .HasForeignKey<GroceryListEntity>("household")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // // Grocerylist item entity
        // modelBuilder.Entity<GroceryListItemEntity>()
        //     .HasOne<GroceryListEntity>()
        //     .WithMany()
        //     .HasForeignKey("grocerylist")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Entity<WeekPlanEntity>()
        //     .HasOne<HouseholdEntity>()
        //     .WithMany()
        //     .HasForeignKey("household")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // // if household delete, delete the tags
        // modelBuilder.Entity<TagEntity>()
        //     .HasOne<HouseholdEntity>()
        //     .WithMany()
        //     .HasForeignKey("household")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Entity<MealEntity>()
        //     .HasOne<HouseholdEntity>()
        //     .WithMany()
        //     .HasForeignKey("household")
        //     .OnDelete(DeleteBehavior.Cascade);
        //
        // modelBuilder.Entity<IngredientEntity>()
        //     .HasOne<MealEntity>()
        //     .WithMany()
        //     .HasForeignKey("meal")
        //     .OnDelete(DeleteBehavior.Cascade);
        
        // time stamp behaviors
        // ConfigureTimestamps<BaseEntity>(modelBuilder);
    }
    
    // private void ConfigureTimestamps<TEntity>(ModelBuilder modelBuilder) where TEntity : BaseEntity
    // {
    //     modelBuilder.Entity<TEntity>()
    //         .Property(e => e.CreatedAt)
    //         .HasColumnType("timestamp");
    //     
    //     modelBuilder.Entity<TEntity>()
    //         .Property(e => e.LastModified)
    //         .HasColumnType("timestamp");
    // }
    
    // public DbSet<MemberEntity> Members { get; set; }
    public DbSet<HouseholdEntity> Household { get; set; }
    // public DbSet<GroceryListEntity> GroceryList { get; set; }
    // public DbSet<GroceryListItemEntity> GroceryListItem { get; set; }
    // public DbSet<WeekPlanEntity> WeekPlan { get; set; }
    // public DbSet<TagEntity> Tag { get; set; }
    // public DbSet<MealEntity> Meal { get; set; }
    // public DbSet<IngredientEntity> Ingredient { get; set; }
    // public DbSet<TodoItem> TodoItems { get; set; }
    public async Task<int> SaveChangesAsync()
    {
        // if an entity is added or modified, the datetime is set.
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified))
        {
            entry.Entity.LastModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.Now;
            }
        }
        return await base.SaveChangesAsync();
    }
}