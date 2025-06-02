using Products.Common.Contracts;
using System.Collections;
using System.Collections.Concurrent;
using System.Text.Json;

namespace Products.Common.Infrastructure;

public class AsyncCrudService<T> : IAsyncCrudService<T> where T : class, IEntity
{
    private readonly ConcurrentDictionary<Guid, T> _storage = new();
    private readonly Func<T, Guid> _idSelector;
    private readonly SemaphoreSlim _semaphore = new(1, 1);

    public AsyncCrudService(Func<T, Guid> idSelector)
    {
        _idSelector = idSelector ?? throw new ArgumentNullException(nameof(idSelector));
    }

    public async Task<bool> CreateAsync(T element)
    {
        if (element == null) return false;
        var id = _idSelector(element);
        var added = _storage.TryAdd(id, element);
        return added;
    }

    public Task<T> ReadAsync(Guid id)
    {
        _storage.TryGetValue(id, out var element);
        return Task.FromResult(element);
    }

    public Task<IEnumerable<T>> ReadAllAsync()
    {
        return Task.FromResult<IEnumerable<T>>(_storage.Values);
    }

    public Task<IEnumerable<T>> ReadAllAsync(int page, int amount)
    {
        var result = _storage.Values
            .Skip((page - 1) * amount)
            .Take(amount);
        return Task.FromResult<IEnumerable<T>>(result);
    }

    public async Task<bool> UpdateAsync(T element)
    {
        if (element == null) return false;
        var id = _idSelector(element);
        _storage[id] = element;
        return true;
    }

    public async Task<bool> RemoveAsync(T element)
    {
        if (element == null) return false;
        var id = _idSelector(element);
        var removed = _storage.TryRemove(id, out _);
        return removed;
    }

    public async Task<bool> SaveAsync(string path)
    {
        try
        {
            await _semaphore.WaitAsync();
            var options = new JsonSerializerOptions { WriteIndented = true };
            var data = _storage.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
            var json = JsonSerializer.Serialize(data, options);
            await File.WriteAllTextAsync(path, json);
            return true;
        }
        catch
        {
            return false;
        }
        finally
        {
            _semaphore.Release();
        }
    }

    public async Task<bool> LoadAsync(string path)
    {
        if (!File.Exists(path)) return false;

        try
        {
            await _semaphore.WaitAsync();
            var json = await File.ReadAllTextAsync(path);
            var data = JsonSerializer.Deserialize<Dictionary<Guid, T>>(json);
            if (data != null)
            {
                foreach (var kvp in data)
                {
                    _storage[kvp.Key] = kvp.Value;
                }
            }
            return true;
        }
        catch
        {
            // Можна додати логування
        }
        finally
        {
            _semaphore.Release();
        }

        return false;
    }

    public IEnumerator<T> GetEnumerator()
    {
        return _storage.Values.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
