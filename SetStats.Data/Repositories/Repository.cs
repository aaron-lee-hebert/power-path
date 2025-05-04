using System;
using Microsoft.EntityFrameworkCore;
using SetStats.Core.Interfaces.Repositories;

namespace SetStats.Data.Repositories;
public class Repository<T>(ApplicationDbContext context) : IRepository<T> where T : class
{
    private readonly DbSet<T> _dbSet = context.Set<T>();

    public async Task<IEnumerable<T>> GetAllAsync() => await _dbSet.ToListAsync();
    public async Task<T?> GetByIdAsync(Guid id) => await _dbSet.FindAsync(id);
    public async Task AddAsync(T entity) => await _dbSet.AddAsync(entity);
    public void Remove(T entity) => _dbSet.Remove(entity);
    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}
