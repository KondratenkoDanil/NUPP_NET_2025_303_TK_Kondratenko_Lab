using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using Products.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductsDbContext>(x => 
    x.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository<LaptopModel>, LaptopRepository>();
builder.Services.AddScoped<IAsyncCrudService<LaptopModel>, LaptopService>();

builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy
            .AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseOpenApi();
app.MapControllers();
app.UseSwaggerUi();
app.UseReDoc(x => x.Path = "/redoc");

app.Run();
