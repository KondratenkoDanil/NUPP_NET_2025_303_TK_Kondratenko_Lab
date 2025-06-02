using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using System.Collections;

namespace Products.Infrastructure.Repositories;

public class LaptopService : IAsyncCrudService<LaptopModel>
{
    public LaptopService(IRepository<LaptopModel> repository)
    {
        Repository = repository;
    }

    public IRepository<LaptopModel> Repository { get; }

    public Task<bool> CreateAsync(LaptopModel element)
    {
        return Repository.InsertAsync(element);
    }

    public IEnumerator<LaptopModel> GetEnumerator()
    {
        return Repository.GetAll().Result.GetEnumerator();
    }

    public Task<bool> LoadAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<LaptopModel>> ReadAllAsync()
    {
        return Repository.GetAll();
    }

    public Task<IEnumerable<LaptopModel>> ReadAllAsync(int page, int count)
    {
        return Repository.GetAll(page, count);
    }

    public Task<LaptopModel?> ReadAsync(Guid id)
    {
        return Repository.GetById(id);
    }

    public Task<bool> RemoveAsync(LaptopModel element)
    {
        return Repository.DeleteAsync(element);
    }

    public Task<bool> SaveAsync(string path)
    {
        return Repository.SaveAsync(path);
    }

    public Task<bool> UpdateAsync(LaptopModel element)
    {
        return Repository.UpdateAsync(element);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
