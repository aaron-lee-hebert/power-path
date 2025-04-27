using static PowerPath.Web.Application.AppEnums;

namespace PowerPath.Web.Data.Entities
{
    public class ExerciseType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public WorkoutCategory Category { get; set; }
        public string? Description { get; set; }
    }
}
