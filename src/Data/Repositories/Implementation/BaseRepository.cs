using System.Linq.Expressions;
using Data.Common.Context;
using Data.Common.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace Data.Common.Repositories.Implementation;

public abstract class BaseRepository<TEntity, TDatabaseContext>(TDatabaseContext context) :
    IBaseRepository<TEntity>
    where TEntity : class
    where TDatabaseContext : BaseContext<TDatabaseContext>
{
    protected readonly TDatabaseContext _context = context;

    public virtual async Task AddAsync(TEntity token)
    {
        await _context.Set<TEntity>().AddAsync(token);

        await _context.SaveChangesAsync();
    }

    public virtual async Task<int> DeleteAsync(Expression<Func<TEntity, bool>> filter)
    {
        _context.RemoveRange(await _context.Set<TEntity>().Where(filter).ToListAsync());
        return await _context.SaveChangesAsync();
    }

    public virtual async Task<TEntity?> FindAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _context.Set<TEntity>()
            .AsNoTracking()
            .FirstOrDefaultAsync(filter);
    }

    public virtual async Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        IQueryable<TEntity> source = _context.Set<TEntity>().AsNoTracking();

        if (filter != null)
        {
            source = source.Where(filter);
        }

        return await source.ToListAsync();
    }

    public virtual async Task UpdateAsync(TEntity token)
    {
        _context.Set<TEntity>().Update(token);
        await _context.SaveChangesAsync();
    }
}