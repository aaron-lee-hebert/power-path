using static SetStats.Web.Application.AppEnums;

namespace SetStats.Web.Data.Entities
{
    public class WorkoutDay
    {
        public Guid Id { get; set; }
        public required Guid UserId { get; set; }
        public required Guid WorkoutWeekId { get; set; }

        public required DateTime WorkoutDate { get; set; }
        public required WorkoutDayStatus Status { get; set; }
        
        public string? Notes { get; set; }

        public required User User { get; set; }
        public required WorkoutWeek WorkoutWeek { get; set; }
    }
}
