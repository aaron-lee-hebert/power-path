using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingProgramCycleRepository(ApplicationDbContext context) : Repository<TrainingProgramCycle>(context), ITrainingProgramCycleRepository
{

}
