using Microsoft.EntityFrameworkCore;
using Products.Common.Entities;
using Products.Common.Extensions;
using Products.Common.Infrastructure;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using Products.Infrastructure.Repositories;

public class Program
{
    public static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<ProductsDbContext>()
            .UseSqlServer("Server=DESKTOP-QUOHQAA\\SQLEXPRESS;Database=productsdb;Trusted_Connection=True;TrustServerCertificate=True;Encrypt=false;")
            .UseMongoDB("mongodb+srv://user:0Password1@cluster0.fuvwccg.mongodb.net/?retryWrites=true&w=majority&appName=Cluster0", "products")
            .Options; 

        var context = new ProductsDbContext(options);

        var laptop = new LaptopModel() 
        { 
            Description = "Description", 
            Id = Guid.NewGuid(), 
            Name = "Laptop HP", 
            Price = 1000, 
            Processor = "Processor",
            RAM = 16 
        };

        context.Database.EnsureCreated();

        var repository = new LaptopRepository(context);
        var service = new LaptopService(repository);

        await service.CreateAsync(laptop);

        var found = await service.ReadAsync(laptop.Id);

        if (found is not null)
        {
            Console.WriteLine($"FOUND {found.Name}");
        }

        foreach (var item in await service.ReadAllAsync())
        {
            Console.WriteLine($"LAPTOP: {item.Name} {item.Price}$");
        }

        laptop.Processor = "New Processor";

        if (await service.UpdateAsync(laptop))
        {
            Console.WriteLine($"UPDATED {laptop.Name}");
        }

        if (await service.RemoveAsync(laptop))
        {
            Console.WriteLine($"DELETED {laptop.Name}");
        }
    }
}