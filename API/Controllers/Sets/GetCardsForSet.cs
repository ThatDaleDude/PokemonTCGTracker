using API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonTcgSdk.Standard.Features.FilterBuilder.Pokemon;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Cards;
using Shared;

namespace API.Controllers.Sets;

[ApiController]
[Route("Sets")]
public class GetCardsForSet(AppDbContext context, PokemonApiClient pokemonClient) : ControllerBase
{
    [HttpGet("{setId}")]
    public async Task<IActionResult> GetAsync(string setId, CancellationToken cancellationToken)
    {
        var filter = PokemonFilterBuilder.CreatePokemonFilter().AddSetId(setId);
        var cards = (await pokemonClient.GetApiResourceAsync<Card>(filter, cancellationToken)).Results;

        var collectedPokedexIdsForSet = await context.CollectedCards
            .Where(x => x.SetId == setId)
            .Select(x => x.PokedexId)
            .ToListAsync(cancellationToken);

        var models = cards.Select(x =>
        {
            var pokedexId = int.TryParse(x.Number, out var number) ? number : 0;
            
            return new CardModel
            {
                PokedexId = pokedexId,
                Name      = x.Name,
                Image     = x.Images.Large,
                Collected = collectedPokedexIdsForSet.Contains(pokedexId)
            };
        }).OrderBy(x => x.PokedexId);

        return Ok(models);
    }
}