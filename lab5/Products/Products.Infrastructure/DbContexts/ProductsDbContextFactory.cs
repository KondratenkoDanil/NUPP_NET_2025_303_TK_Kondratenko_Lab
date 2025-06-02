using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Products.Infrastructure.DbContexts;

public class ProductsDbContextFactory : IDesignTimeDbContextFactory<ProductsDbContext>
{
    public ProductsDbContext CreateDbContext(string[] args)
    {
        var options = new DbContextOptionsBuilder()
            .UseSqlServer("Server=localhost\\SQLEXPRESS;Database=products;Trusted_Connection=True;Encrypt=False;TrustServerCertificate=True;")
            .Options;
        
        return new ProductsDbContext(options);
    }
}