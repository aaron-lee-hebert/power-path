using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Data.Entities;

namespace SetStats.Data.Configurations;

public class WorkoutDayConfiguration : IEntityTypeConfiguration<WorkoutDay>
{
    public void Configure(EntityTypeBuilder<WorkoutDay> builder)
    {
        _ = builder.ToTable("workout_days");

        _ = builder.HasIndex(wd => new { wd.UserId, wd.WorkoutWeekId, wd.WorkoutDate }).IsUnique();
    }
}
