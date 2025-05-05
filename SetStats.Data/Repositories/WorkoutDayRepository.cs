using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class WorkoutDayRepository(ApplicationDbContext context) : Repository<WorkoutDay>(context), IWorkoutDayRepository
{

}
