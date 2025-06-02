using Newtonsoft.Json;
using Products.Common.Contracts;

public class CrudService<T> : ICrudService<T> where T : class, IEntity
{
    private Dictionary<Guid, T> _data = new Dictionary<Guid, T>();

    public void Create(T element)
    {
        _data[element.Id] = element;
    }

    public T Read(Guid id)
    {
        if (_data.TryGetValue(id, out T element))
        {
            return element;
        }
        return null;
    }

    public IEnumerable<T> ReadAll()
    {
        return _data.Values;
    }

    public void Update(T element)
    {
        if (_data.ContainsKey(element.Id))
        {
            _data[element.Id] = element;
        }
    }

    public void Remove(T element)
    {
        _data.Remove(element.Id);
    }

    public void Save(string path)
    {
        try
        {
            string json = JsonConvert.SerializeObject(_data);
            File.WriteAllText(path, json);
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error saving data: {ex.Message}");
        }
    }

    public void Load(string path)
    {
        try
        {
            string json = File.ReadAllText(path);
            _data = JsonConvert.DeserializeObject<Dictionary<Guid, T>>(json)!;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error loading data: {ex.Message}");
        }
    }
}
