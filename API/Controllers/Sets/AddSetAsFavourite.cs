using API.Context;
using API.Context.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Sets;

[ApiController]
[Route("Sets")]
public class AddSetAsFavourite(AppDbContext context) : ControllerBase
{
    [HttpPost("{setId}/favourite")]
    public async Task<IActionResult> PostAsync(string setId, CancellationToken cancellationToken)
    {
        await context.AddAsync(new FavouriteSet
        {
            Id    = Guid.NewGuid(),
            SetId = setId
        }, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}