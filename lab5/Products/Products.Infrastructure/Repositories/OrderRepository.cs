using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.Repositories;

public class OrderRepository : IRepository<OrderModel>
{
    public OrderRepository(ProductsDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public ProductsDbContext DbContext { get; }

    public async Task<bool> DeleteAsync(OrderModel element)
    {
        DbContext.Remove(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<OrderModel>> GetAll()
    {
        return await DbContext.Orders
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<OrderModel>> GetAll(int page, int count)
    {
        return await DbContext.Orders
            .Skip(page * count)
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<OrderModel?> GetById(Guid id)
    {
        return await DbContext.Orders.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> InsertAsync(OrderModel element)
    {
        DbContext.Add(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveAsync(string path)
    {
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(OrderModel element)
    {
        DbContext.Update(element);
        return await DbContext.SaveChangesAsync() > 0;
    }
}
