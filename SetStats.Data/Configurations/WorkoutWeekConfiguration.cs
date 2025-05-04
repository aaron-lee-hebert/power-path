using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Data.Entities;

namespace SetStats.Data.Configurations;

public class WorkoutWeekConfiguration : IEntityTypeConfiguration<WorkoutWeek>
{
    public void Configure(EntityTypeBuilder<WorkoutWeek> builder)
    {
        _ = builder.ToTable("workout_weeks");

        _ = builder.HasIndex(ww => new { ww.TrainingCycleId, ww.WeekNumber }).IsUnique();
    }
}
