using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.DbContexts;

public sealed class ProductsDbContext : DbContext
{
    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<LaptopModel> Laptops { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}
