using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class ExerciseTypeRepository(ApplicationDbContext context) : Repository<ExerciseType>(context), IExerciseTypeRepository
{

}
