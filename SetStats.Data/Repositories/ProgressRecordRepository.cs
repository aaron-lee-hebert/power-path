using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class ProgressRecordRepository(ApplicationDbContext context) : Repository<ProgressRecord>(context), IProgressRecordRepository
{

}
