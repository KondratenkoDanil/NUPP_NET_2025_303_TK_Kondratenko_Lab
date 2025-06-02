namespace Products.Common.Contracts;

public interface IRepository<T>
{
    Task<bool> InsertAsync(T element);
    Task<T?> GetById(Guid id);
    Task<IEnumerable<T>> GetAll();
    Task<IEnumerable<T>> GetAll(int page, int count);
    Task<bool> UpdateAsync(T element);
    Task<bool> DeleteAsync(T element);
    Task<bool> SaveAsync(string path);
}
