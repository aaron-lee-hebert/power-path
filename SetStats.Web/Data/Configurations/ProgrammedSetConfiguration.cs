using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Web.Data.Entities;

namespace SetStats.Web.Data.Configurations
{
    public class ProgrammedSetConfiguration : IEntityTypeConfiguration<ProgrammedSet>
    {
        public void Configure(EntityTypeBuilder<ProgrammedSet> builder)
        {
            builder.ToTable("programmed_sets");
            
            builder.HasIndex(ps => new { ps.WorkoutDayId, ps.ExerciseTypeId, ps.SetOrder })
                .IsUnique();

            builder.Property(ps => ps.PercentageOfTrainingMax)
                .HasPrecision(4, 2);
        }
    }
}
