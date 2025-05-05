using SetStats.Core.Entities;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class UserMaxRepository(ApplicationDbContext context) : Repository<UserMax>(context), IUserMaxRepository
{

}
