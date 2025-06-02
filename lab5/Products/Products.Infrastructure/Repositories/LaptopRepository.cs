using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.Repositories;

public class LaptopRepository : IRepository<LaptopModel>
{
    public LaptopRepository(ProductsDbContext dbContext)
    {
        DbContext = dbContext;
    }

    public ProductsDbContext DbContext { get; }

    public async Task<bool> DeleteAsync(LaptopModel element)
    {
        DbContext.Remove(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<IEnumerable<LaptopModel>> GetAll()
    {
        return await DbContext.Laptops
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<LaptopModel>> GetAll(int page, int count)
    {
        return await DbContext.Laptops
            .Skip(page * count)
            .Take(count)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<LaptopModel?> GetById(Guid id)
    {
        return await DbContext.Laptops.AsNoTracking().FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task<bool> InsertAsync(LaptopModel element)
    {
        DbContext.Add(element);
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> SaveAsync(string path)
    {
        return await DbContext.SaveChangesAsync() > 0;
    }

    public async Task<bool> UpdateAsync(LaptopModel element)
    {
        DbContext.Update(element);
        return await DbContext.SaveChangesAsync() > 0;
    }
}
