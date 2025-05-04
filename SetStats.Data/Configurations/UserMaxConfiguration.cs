using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Data.Entities;

namespace SetStats.Data.Configurations;

public class UserMaxConfiguration : IEntityTypeConfiguration<UserMax>
{
    public void Configure(EntityTypeBuilder<UserMax> builder)
    {
        _ = builder.ToTable("user_maxes");

        _ = builder.HasIndex(um => new { um.UserId, um.ExerciseTypeId, um.TrainingCycleId })
            .IsUnique();

        _ = builder.Property(um => um.ActualOneRepMax)
            .HasPrecision(6, 2);

        _ = builder.Property(um => um.EstimatedOneRepMax)
            .HasPrecision(6, 2);

        _ = builder.Property(um => um.RoundingFactor)
            .HasPrecision(4, 2);
    }
}
