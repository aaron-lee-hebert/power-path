using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Core.Entities;

namespace SetStats.Data.Configurations;
public class TrainingProgramConfiguration : IEntityTypeConfiguration<TrainingProgram>
{
    public void Configure(EntityTypeBuilder<TrainingProgram> builder)
    {
        _ = builder.ToTable("training_programs");

        _ = builder.HasIndex(tp => new { tp.UserId, tp.Name }).IsUnique();
    }
}
