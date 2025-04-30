using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data.Configurations
{
    public class UserMaxConfiguration : IEntityTypeConfiguration<UserMax>
    {
        public void Configure(EntityTypeBuilder<UserMax> builder)
        {
            builder.ToTable("user_maxes");

            builder.HasIndex(um => new { um.UserId, um.ExerciseTypeId, um.TrainingCycleId })
                .IsUnique();

            builder.Property(um => um.ActualOneRepMax)
                .HasPrecision(6, 2);

            builder.Property(um => um.EstimatedOneRepMax)
                .HasPrecision(6, 2);

            builder.Property(um => um.RoundingFactor)
                .HasPrecision(4, 2);
        }
    }
}
