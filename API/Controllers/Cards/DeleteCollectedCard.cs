using API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers.Cards;

[ApiController]
[Route("Sets/{setId}")]
public class DeleteCollectedCard(AppDbContext context) : ControllerBase
{
    [HttpDelete("{pokedexId:int}")]
    public async Task<IActionResult> DeleteAsync(int pokedexId, [FromRoute] string setId, CancellationToken cancellationToken)
    {
        var collectedCard = await context.CollectedCards
            .Where(x => x.SetId == setId)
            .Where(x => x.PokedexId == pokedexId)
            .SingleOrDefaultAsync(cancellationToken);

        if (collectedCard == null)
        {
            return NotFound($"Could not find card with pokedexId {pokedexId} in set {setId}");
        }

        context.Remove(collectedCard);
        await context.SaveChangesAsync(cancellationToken);

        return NoContent();
    }
}