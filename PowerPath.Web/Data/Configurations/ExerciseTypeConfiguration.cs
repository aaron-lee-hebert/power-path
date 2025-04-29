using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PowerPath.Web.Data.Entities;

namespace PowerPath.Web.Data.Configurations
{
    public class ExerciseTypeConfiguration
    {
        public class ExerciseTypeConfigurations : IEntityTypeConfiguration<ExerciseType>
        {
            public void Configure(EntityTypeBuilder<ExerciseType> builder)
            {
                builder.ToTable("exercise_types");
            }
        }
    }
}
