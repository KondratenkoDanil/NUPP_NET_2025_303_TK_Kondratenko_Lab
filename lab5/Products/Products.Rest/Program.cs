using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Products.Common.Contracts;
using Products.Infrastructure.DbContexts;
using Products.Infrastructure.Models;
using Products.Infrastructure.Repositories;
using Products.Rest;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddOpenApiDocument(config =>
{
    config.AddSecurity("Bearer", new NSwag.OpenApiSecurityScheme
    {
        Type = NSwag.OpenApiSecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        In = NSwag.OpenApiSecurityApiKeyLocation.Header,
        Name = "Authorization",
        Description = "Type 'Bearer' followed by a space and your token"
    });
    
    config.OperationProcessors.Add(
        new NSwag.Generation.Processors.Security.AspNetCoreOperationSecurityScopeProcessor("Bearer"));
});

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ProductsDbContext>(x => 
    x.UseSqlServer(connectionString));

builder.Services.AddScoped<IRepository<LaptopModel>, LaptopRepository>();
builder.Services.AddScoped<IAsyncCrudService<LaptopModel>, LaptopService>();

builder.Services.AddControllers();

builder.Services.AddAuthentication(BearerTokenDefaults.AuthenticationScheme)
    .AddBearerToken();

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ProductsDbContext>()
    .AddDefaultTokenProviders();


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

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    string[] roles = [Roles.Admin, Roles.Moderator];

    foreach (var role in roles)
        if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
}


app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseOpenApi();
app.UseSwaggerUi();
app.UseReDoc(x => x.Path = "/redoc");

app.MapControllers();
app.MapIdentityApi<IdentityUser>();

app.Run();
