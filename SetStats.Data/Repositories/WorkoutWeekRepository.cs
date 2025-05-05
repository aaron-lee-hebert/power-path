using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class WorkoutWeekRepository(ApplicationDbContext context) : Repository<WorkoutWeek>(context), IWorkoutWeekRepository
{

}
