using static SetStats.Web.Application.AppEnums;

namespace SetStats.Web.Data.Entities
{
    public class ExerciseType
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public WorkoutCategory Category { get; set; }
        public string? Description { get; set; }
    }
}
