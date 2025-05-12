using SetStats.Core.Entities;

namespace SetStats.Core.Interfaces.Repositories;
public interface ITrainingCycleRepository : IRepository<TrainingCycle>
{
    public Task<IEnumerable<TrainingCycle>> GetByUserIdAsync(Guid userId);
}
