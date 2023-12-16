using System.Linq.Expressions;

namespace Domain.Interfaces;

public interface IGeneric<T>
    where T : class
{
    Task<T> GetByIdAsync(int id);
    Task<T> GetByNameAsync(string name);
    Task<IEnumerable<T>> GetAllAsync();
    IEnumerable<T> Find(Expression<Func<T, bool>> expression);
    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
}
