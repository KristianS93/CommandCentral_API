using Domain.Entities.Household;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Persistence.Configurations;

public class HouseholdConfiguration : IEntityTypeConfiguration<HouseholdEntity>
{
    public void Configure(EntityTypeBuilder<HouseholdEntity> builder)
    {
        // configure timestamp
        builder.Property(e => e.CreatedAt)
            .HasColumnType("timestamp");
        builder.Property(e => e.LastModified)
            .HasColumnType("timestamp");
    }
}