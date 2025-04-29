using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data.Configurations
{
    public class CompletedSetConfiguration : IEntityTypeConfiguration<CompletedSet>
    {
        public void Configure(EntityTypeBuilder<CompletedSet> builder)
        {
            builder.ToTable("completed_sets");

            builder.Property(cs => cs.ActualWeight)
                .HasPrecision(6, 2);

            builder.HasIndex(cs => new { cs.ProgrammedSetId })
                .IsUnique();
        }
    }
}
