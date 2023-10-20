using Domain.Entities.GroceryList;
using Domain.Entities.Household;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class GroceryListConfiguration : IEntityTypeConfiguration<GroceryListEntity>
{
    public void Configure(EntityTypeBuilder<GroceryListEntity> builder)
    {
        // Only 1 grocerylist per household allowed, householdId should be unique
        builder.HasIndex(key => key.HouseholdId)
            .IsUnique();
        
        // one to one relationship, with on delete cascade
        builder.HasOne<HouseholdEntity>()
            .WithOne()
            .HasForeignKey<GroceryListEntity>("household")
            .OnDelete(DeleteBehavior.Cascade);
        
        // configure timestamp
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");
        builder.Property(e => e.LastModified)
            .HasColumnType("timestamp");
    }
}