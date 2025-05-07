using Microsoft.EntityFrameworkCore;
using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class TrainingProgramRepository(ApplicationDbContext context) : Repository<TrainingProgram>(context), ITrainingProgramRepository
{
    public async Task<IEnumerable<TrainingProgram>> GetByUserIdAsync(Guid userId) => await dbSet
            .Where(tp => tp.UserId == userId)
            .ToListAsync();
}
