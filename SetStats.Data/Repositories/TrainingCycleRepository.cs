using Microsoft.EntityFrameworkCore;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingCycleRepository(ApplicationDbContext context) : Repository<TrainingCycle>(context), ITrainingCycleRepository
{
    public async Task<IEnumerable<TrainingCycle>> GetByUserIdAsync(Guid userId) => await dbSet
            .Where(tp => tp.UserId == userId)
            .AsNoTracking()
            .ToListAsync();
}
