using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Web.Data.Entities;

namespace SetStats.Web.Data.Configurations
{
    public class ProgressRecordConfiguration : IEntityTypeConfiguration<ProgressRecord>
    {
        public void Configure(EntityTypeBuilder<ProgressRecord> builder)
        {
            builder.ToTable("progress_records");

            builder.HasIndex(pr => new { pr.UserId, pr.ExerciseTypeId, pr.Weight, pr.Reps })
                .IsUnique();

            builder.Property(pr => pr.Weight)
                .HasPrecision(6, 2);
        }
    }
}
