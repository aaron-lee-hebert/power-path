using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingCycleRepository(ApplicationDbContext context) : Repository<TrainingCycle>(context), ITrainingCycleRepository
{

}
