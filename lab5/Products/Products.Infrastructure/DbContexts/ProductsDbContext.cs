using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Products.Infrastructure.Models;

namespace Products.Infrastructure.DbContexts;

public sealed class ProductsDbContext : IdentityDbContext<IdentityUser>
{
    public ProductsDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<LaptopModel> Laptops { get; set; }
    public DbSet<OrderModel> Orders { get; set; }
    public DbSet<CustomerModel> Customers { get; set; }
}
