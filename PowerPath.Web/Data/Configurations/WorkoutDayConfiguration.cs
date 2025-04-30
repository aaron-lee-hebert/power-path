using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data.Configurations
{
    public class WorkoutDayConfiguration : IEntityTypeConfiguration<WorkoutDay>
    {
        public void Configure(EntityTypeBuilder<WorkoutDay> builder)
        {
            builder.ToTable("workout_days");

            builder.HasIndex(wd => new { wd.UserId, wd.WorkoutWeekId, wd.WorkoutDate })
                .IsUnique();
        }
    }
}
