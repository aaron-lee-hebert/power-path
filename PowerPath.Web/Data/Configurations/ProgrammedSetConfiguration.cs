using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data.Configurations
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
