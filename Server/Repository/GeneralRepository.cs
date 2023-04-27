using Microsoft.EntityFrameworkCore;
using Server.Data;
using Server.Repository.Contracts;

namespace Server.Repository;
public class GeneralRepository<TEntity, TKey, TContext> : IGeneralRepository<TEntity, TKey> 
    where TEntity : class 
    where TContext : ManagementContext 
{
    protected TContext _context;
        
    public GeneralRepository(TContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();

    }

    public async Task<TEntity?> GetByIdAsync(TKey key)
    {
        return await _context.Set<TEntity>().FindAsync(key);
    }

    public async Task InsertAsync(TEntity entity)
    {
        await _context.Set<TEntity>().AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TEntity entity)
    {
        _context.Entry(entity).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(TKey key)
    {
        var entity = await GetByIdAsync(key);
        _context.Set<TEntity>().Remove(entity!);
        await _context.SaveChangesAsync();
    }
}
