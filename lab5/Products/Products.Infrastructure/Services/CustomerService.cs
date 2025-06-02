using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using System.Collections;

namespace Products.Infrastructure.Repositories;

public class CustomerService : IAsyncCrudService<CustomerModel>
{
    public CustomerService(IRepository<CustomerModel> repository)
    {
        Repository = repository;
    }

    public IRepository<CustomerModel> Repository { get; }

    public Task<bool> CreateAsync(CustomerModel element)
    {
        return Repository.InsertAsync(element);
    }

    public IEnumerator<CustomerModel> GetEnumerator()
    {
        return Repository.GetAll().Result.GetEnumerator();
    }

    public Task<bool> LoadAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<CustomerModel>> ReadAllAsync()
    {
        return Repository.GetAll();
    }

    public Task<IEnumerable<CustomerModel>> ReadAllAsync(int page, int count)
    {
        return Repository.GetAll(page, count);
    }

    public Task<CustomerModel?> ReadAsync(Guid id)
    {
        return Repository.GetById(id);
    }

    public Task<bool> RemoveAsync(CustomerModel element)
    {
        return Repository.DeleteAsync(element);
    }

    public Task<bool> SaveAsync(string path)
    {
        return Repository.SaveAsync(path);
    }

    public Task<bool> UpdateAsync(CustomerModel element)
    {
        return Repository.UpdateAsync(element);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
