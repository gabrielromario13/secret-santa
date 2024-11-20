using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
{
    private readonly DbContext _context;
    private readonly DbSet<T> _dbSet;

    protected BaseRepository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<T>();
    }

    public async Task<int?> Add(T entity)
    {
        try
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        catch
        {
            return null;
        }

        return entity.Id;
    }

    public async Task Update(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
    }

    public async Task Remove(T entity)
    {
        var result = await _dbSet
            .FirstOrDefaultAsync(x => x.Id == entity.Id);

        if (result is not null)
            _dbSet.Remove(result);
    }

    public async Task<T?> GetById(
        int id,
        IEnumerable<Expression<Func<T, object>>>? includeProperties = default!)
    {
        var query = _dbSet.AsNoTracking();

        if (includeProperties is null) 
            return await query.FirstOrDefaultAsync(x => x.Id == id);

        query = includeProperties.Aggregate(query, (current, property)
            => current.Include(property));

        return await query.FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<T?> GetSingle(
        Expression<Func<T, bool>>? predicate = default!,
        IEnumerable<Expression<Func<T, object>>>? includeProperties = default!)
    {
        var query = _dbSet.AsNoTracking();

        if (predicate is not null)
            query = query.Where(predicate);

        if (includeProperties is null) 
            return await query.FirstOrDefaultAsync();

        query = includeProperties.Aggregate(query, (current, property)
            => current.Include(property));

        return await query.FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>>? predicate = null,
        IEnumerable<Expression<Func<T, object>>>? includeProperties = default)
    {
        var query = _dbSet.AsNoTracking()
            .AsQueryable();

        if (predicate != null)
            query = query.Where(predicate);

        if (includeProperties == null)
            return await query.ToListAsync();

        query = includeProperties.Aggregate(query, (current, property) 
            => current.Include(property));

        return await query.ToListAsync();
    }

    public async Task<long> Count()
    {
        var result = await _dbSet.CountAsync();
        return result;
    }
}