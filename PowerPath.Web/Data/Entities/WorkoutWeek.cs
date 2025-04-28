using System.ComponentModel.DataAnnotations;
using static PowerPath.Web.Application.AppEnums;

namespace PowerPath.Web.Data.Entities
{
    public class WorkoutWeek
    {
        public Guid Id { get; set; }
        public required Guid TrainingCycleId { get; set; }
        [Range(1, 4)]
        public required int WeekNumber { get; set; }
        public required WeekType WeekType { get; set; }
        
        public required TrainingCycle TrainingCycle { get; set; }
    }
}
