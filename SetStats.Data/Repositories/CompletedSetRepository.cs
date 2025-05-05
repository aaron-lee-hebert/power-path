using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class CompletedSetRepository(ApplicationDbContext context) : Repository<CompletedSet>(context), ICompletedSetRepository
{

}
