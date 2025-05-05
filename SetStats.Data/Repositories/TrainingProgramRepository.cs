using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingProgramRepository(ApplicationDbContext context) : Repository<TrainingProgram>(context), ITrainingProgramRepository
{

}
