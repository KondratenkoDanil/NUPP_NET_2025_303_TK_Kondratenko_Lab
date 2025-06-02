using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Products.Common.Contracts;
using Products.Infrastructure.Models;

namespace Products.Rest.Controllers;

[Route("/laptops")]
public class LaptopController : Controller
{
    [HttpGet]
    public async Task<IResult> GetAll(
        [FromServices] IAsyncCrudService<LaptopModel> service
        )
    {
        return Results.Ok(await service.ReadAllAsync());
    }
    
    [HttpGet("{id:guid}")]
    public async Task<IResult> Get(
        [FromRoute] Guid id,
        [FromServices] IAsyncCrudService<LaptopModel> service
    )
    {
        var found = await service.ReadAsync(id);
        
        if (found is not null)
            return Results.Ok(found);
        
        return Results.NotFound();

    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPost]
    public async Task<IResult> Create(
        [FromBody] LaptopModel value,
        [FromServices] IAsyncCrudService<LaptopModel> service
    )
    {
        var result = await service.CreateAsync(value);

        if (result)
            return Results.Created("/", value);

        return Results.BadRequest();
    }

    [Authorize(Roles = Roles.Admin)]
    [HttpPatch("{id:guid}")]
    public async Task<IResult> Update(
        [FromRoute] Guid id,
        [FromBody] LaptopModel value,
        [FromServices] IAsyncCrudService<LaptopModel> service
    )
    {
        value.Id = id;
        
        var result = await service.UpdateAsync(value);
        
        if (result is false)
            return Results.BadRequest();
        
        var found = await service.ReadAsync(id);
        
        if (found is not null)
            return Results.Ok(found);
        
        return Results.BadRequest();

    }
    
    [Authorize(Roles = Roles.Admin)]
    [HttpDelete("{id:guid}")]
    public async Task<IResult> Delete(
        [FromRoute] Guid id,
        [FromServices] IAsyncCrudService<LaptopModel> service
    )
    {
        var found = await service.ReadAsync(id);
        
        if (found is null)
            return Results.NotFound();
        
        var result = await service.RemoveAsync(found);

        if (result)
            return Results.Ok();

        return Results.BadRequest();
    }
    
    [Authorize]
    [HttpPost("/role/{role}")]
    public async Task<IResult> Grant(
        [FromRoute] string role,
        [FromServices] UserManager<IdentityUser> userManager)
    {
        var user = await userManager.GetUserAsync(User);
        if (!await userManager.IsInRoleAsync(user, role))
        {
            await userManager.AddToRoleAsync(user, role);
            return Results.Ok();
        }
            
        return Results.BadRequest();
    }
}