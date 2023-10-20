using Domain.Entities.GroceryList;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class GroceryListItemConfiguration : IEntityTypeConfiguration<GroceryListItemEntity>
{
    public void Configure(EntityTypeBuilder<GroceryListItemEntity> builder)
    {
        
        // one to one relationship, with on delete cascade
        builder.HasOne<GroceryListEntity>()
            .WithMany()
            .HasForeignKey("grocerylist")
            .OnDelete(DeleteBehavior.Cascade);
        
        // configure timestamp
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");
        builder.Property(e => e.LastModified)
            .HasColumnType("timestamp");
    }
}