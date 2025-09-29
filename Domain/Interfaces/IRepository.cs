namespace Taller_HU3.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T> InsertAsync(T  entity);

    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetOneAsync(T entity);
    Task<T> UpdateAsync(T  entity);
    Task<T> DeleteAsync(T  entity);
    
}