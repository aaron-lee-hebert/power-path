using SetStats.Core.Entities;

namespace SetStats.Core.Interfaces.Repositories;
public interface ITrainingProgramRepository : IRepository<TrainingProgram>
{
    public Task<IEnumerable<TrainingProgram>> GetByUserIdAsync(Guid userId);
}
