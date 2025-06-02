
namespace Products.Common.Contracts;

public interface ICrudService<T>
{
    void Create(T element);
    T Read(Guid id);
    IEnumerable<T> ReadAll();
    void Update(T element);
    void Remove(T element);
    void Save(string path);
    void Load(string path);
}
