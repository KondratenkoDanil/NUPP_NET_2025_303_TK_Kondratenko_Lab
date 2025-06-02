using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using System.Collections;

namespace Products.Infrastructure.Repositories;

public class OrderService : IAsyncCrudService<OrderModel>
{
    public OrderService(IRepository<OrderModel> repository)
    {
        Repository = repository;
    }

    public IRepository<OrderModel> Repository { get; }

    public Task<bool> CreateAsync(OrderModel element)
    {
        return Repository.InsertAsync(element);
    }

    public IEnumerator<OrderModel> GetEnumerator()
    {
        return Repository.GetAll().Result.GetEnumerator();
    }

    public Task<bool> LoadAsync(string path)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<OrderModel>> ReadAllAsync()
    {
        return Repository.GetAll();
    }

    public Task<IEnumerable<OrderModel>> ReadAllAsync(int page, int count)
    {
        return Repository.GetAll(page, count);
    }

    public Task<OrderModel?> ReadAsync(Guid id)
    {
        return Repository.GetById(id);
    }

    public Task<bool> RemoveAsync(OrderModel element)
    {
        return Repository.DeleteAsync(element);
    }

    public Task<bool> SaveAsync(string path)
    {
        return Repository.SaveAsync(path);
    }

    public Task<bool> UpdateAsync(OrderModel element)
    {
        return Repository.UpdateAsync(element);
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
