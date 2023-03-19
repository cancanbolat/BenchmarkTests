using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BenchmarkTests;

public class Repository<T> where T : class
{
    private readonly AppDbContext _dbContext;

    public Repository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public DbSet<T> Table => _dbContext.Set<T>();

    public async Task<T> GetSingleAsync(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(expression);
    }
    public T GetSingle(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.AsQueryable();
        if (!tracking)
            query = query.AsNoTracking();
        return query.FirstOrDefault(expression)!;
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> expression, bool tracking = true)
    {
        var query = Table.Where(expression);

        if (!tracking)
            query = query.AsNoTracking();

        return query;
    }

    public async Task<T> GetWhereAsync(Expression<Func<T, bool>>? predicate, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (predicate != null) query = query.Where(predicate);

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>>? predicate, IList<string>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null) query = query.Where(predicate);

        return await query.ToListAsync();
    }
    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? predicate, IList<string>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null) query = query.Where(predicate);

        return query.ToList();
    }

    public async Task<T> GetByPredicateAsync(Expression<Func<T, bool>>? predicate, IList<string>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null) query = query.Where(predicate);

        return await query.FirstOrDefaultAsync();
    }
    public T GetByPredicate(Expression<Func<T, bool>>? predicate, IList<string>? includes = null, bool disableTracking = true)
    {
        IQueryable<T> query = _dbContext.Set<T>();

        if (disableTracking) query = query.AsNoTracking();

        if (includes != null)
        {
            query = includes.Aggregate(query, (current, include) => current.Include(include));
        }

        if (predicate != null) query = query.Where(predicate);

        return query.FirstOrDefault()!;
    }
}