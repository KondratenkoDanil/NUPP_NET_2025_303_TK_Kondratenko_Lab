using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.Repositories;

public class CustomerRepository : IRepository<CustomerModel>
{
    public CustomerRepository(ProductsDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public ProductsDbContext DbContext { get; }

    public async Task<bool> DeleteAsync(CustomerModel element)
    {
        DbContext.Remove(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<CustomerModel>> GetAll()
    {
        return await DbContext.Customers
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<CustomerModel>> GetAll(int page, int count)
    {
        return await DbContext.Customers
            .Skip(page * count)
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<CustomerModel?> GetById(Guid id)
    {
        return await DbContext.Customers.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> InsertAsync(CustomerModel element)
    {
        DbContext.Add(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveAsync(string path)
    {
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(CustomerModel element)
    {
        DbContext.Update(element);
        return await DbContext.SaveChangesAsync() > 0;
    }
}
