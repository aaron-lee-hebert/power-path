using Microsoft.EntityFrameworkCore;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class Repository<T>(ApplicationDbContext context, DbSet<T> dbSet) : IRepository<T> where T : class
{
    public async Task<IEnumerable<T>> GetAllAsync() => await dbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(int id) => await dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await dbSet.AddAsync(entity);
    public void Remove(T entity) => dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
