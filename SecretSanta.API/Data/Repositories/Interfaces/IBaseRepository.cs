using System.Linq.Expressions;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    Task<int?> Add(T entity);
    Task Update(T entity);
    Task Remove(T entity);
    Task<T?> GetById(int id, IEnumerable<Expression<Func<T, object>>>? includeProperties = default!);
    Task<T?> GetSingle(
        Expression<Func<T, bool>> predicate = default!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = default!);
    Task<IEnumerable<T>> Get(
        Expression<Func<T, bool>> predicate = default!,
        IEnumerable<Expression<Func<T, object>>> includeProperties = default!);
    Task<long> Count();
}