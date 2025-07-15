using API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Sets;

[ApiController]
[Route("Sets")]
public class RemoveSetAsFavourite(AppDbContext context) : ControllerBase
{
    [HttpDelete("{setId}/favourite")]
    public async Task<IActionResult> DeleteAsync(string setId, CancellationToken cancellationToken)
    {
        var favouriteSet = await context.FavouriteSets.SingleOrDefaultAsync(x => x.SetId == setId, cancellationToken);

        if (favouriteSet == null)
        {
            return NotFound($"Couldn't find favourite set with id: {setId}");
        }

        context.Remove(favouriteSet);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}