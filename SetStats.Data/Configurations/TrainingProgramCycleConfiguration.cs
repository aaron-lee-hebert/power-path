using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Core.Entities;

namespace SetStats.Data.Configurations;
public class TrainingProgramCycleConfiguration : IEntityTypeConfiguration<TrainingProgramCycle>
{
    public void Configure(EntityTypeBuilder<TrainingProgramCycle> builder)
    {
        _ = builder.ToTable("training_program_cycles");

        _ = builder.HasIndex(tpc => new { tpc.TrainingProgramId, tpc.TrainingCycleId })
                .IsUnique();
    }
}
