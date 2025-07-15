using API.Context;
using API.Context.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers.Cards;

[ApiController]
[Route("Sets/{setId}")]
public class CreateCollectedCard(AppDbContext context) : ControllerBase
{
    [HttpPost("{pokedexId:int}/favourite")]
    public async Task<IActionResult> PostAsync(int pokedexId, [FromRoute] string setId, CancellationToken cancellationToken)
    {
        await context.CollectedCards.AddAsync(new CollectedCard
        {
            Id        = Guid.NewGuid(),
            SetId     = setId,
            PokedexId = pokedexId,
            DateAdded = DateOnly.FromDateTime(DateTime.Now)
        }, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);

        return Ok();
    }
}