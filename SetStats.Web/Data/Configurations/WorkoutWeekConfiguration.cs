using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Web.Data.Entities;

namespace SetStats.Web.Data.Configurations
{
    public class WorkoutWeekConfiguration : IEntityTypeConfiguration<WorkoutWeek>
    {
        public void Configure(EntityTypeBuilder<WorkoutWeek> builder)
        {
            builder.ToTable("workout_weeks");

            builder.HasIndex(ww => new { ww.TrainingCycleId, ww.WeekNumber })
                .IsUnique();
        }
    }
}
