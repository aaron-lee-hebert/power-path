using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Core.Entities;

namespace SetStats.Data.Configurations;

public class ProgressRecordConfiguration : IEntityTypeConfiguration<ProgressRecord>
{
    public void Configure(EntityTypeBuilder<ProgressRecord> builder)
    {
        _ = builder.ToTable("progress_records");

        _ = builder.HasIndex(pr => new { pr.UserId, pr.ExerciseTypeId, pr.Weight, pr.Reps }).IsUnique();

        _ = builder.Property(pr => pr.Weight).HasPrecision(6, 2);
    }
}
