namespace Alunos.Domain.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetAsync(int id);
    Task<T> PostAsync(T entity);
    Task<T> PutAsync(T entity);
    Task DeleteAsync(T entity);
}
