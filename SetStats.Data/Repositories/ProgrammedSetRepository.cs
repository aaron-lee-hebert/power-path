using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class ProgrammedSetRepository(ApplicationDbContext context) : Repository<ProgrammedSet>(context), IProgrammedSetRepository
{

}
