using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SetStats.Core.Entities;

namespace SetStats.Data.Configurations;

public class CompletedSetConfiguration : IEntityTypeConfiguration<CompletedSet>
{
    public void Configure(EntityTypeBuilder<CompletedSet> builder)
    {
        _ = builder.ToTable("completed_sets");

        _ = builder.Property(cs => cs.ActualWeight)
            .HasPrecision(6, 2);

        _ = builder.HasIndex(cs => new { cs.ProgrammedSetId })
            .IsUnique();
    }
}
