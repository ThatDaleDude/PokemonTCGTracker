using API.Context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PokemonTcgSdk.Standard.Features.FilterBuilder.Set;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients;
using PokemonTcgSdk.Standard.Infrastructure.HttpClients.Set;
using Shared;
using Shared.GetSets;

namespace API.Controllers.Sets;

[ApiController]
[Route("Sets")]
public class GetSets(AppDbContext context, PokemonApiClient pokemonClient) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync([FromQuery] GetSetsQuery query, CancellationToken cancellationToken)
    {
        var filter = SetFilterBuilder.CreateSetFilter();

        if (!string.IsNullOrWhiteSpace(query.SearchText))
        {
            filter.AddName(query.SearchText);
        }

        var setsResponse = await pokemonClient.GetApiResourceAsync<Set>(query.PageSize, query.CurrentPage, filter, cancellationToken);
        var favouriteSetIds = await context.FavouriteSets
            .Select(x => x.SetId)
            .ToListAsync(cancellationToken);
        
        var models = setsResponse.Results
            .Select(x => new SetModel
            {
                Name = x.Name,
                ExternalId = x.Id,
                Symbol = x.Images.Symbol,
                Logo = x.Images.Logo,
                IsFavourite = favouriteSetIds.Contains(x.Id)
            })
            .ToList();

        var response = new GetSetsResponse
        {
            Sets      = models,
            TotalSets = setsResponse.TotalCount
        };

        return Ok(response);
    }
}