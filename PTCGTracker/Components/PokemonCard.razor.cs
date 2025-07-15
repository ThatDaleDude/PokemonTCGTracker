using Microsoft.AspNetCore.Components;

namespace PTCGTracker.Components;

public partial class PokemonCard : ComponentBase
{
    [Parameter] public required string Name { get; set; }
    [Parameter] public required Uri ImageUrl { get; set; }
    [Parameter] public int PokedexId { get; set; }
    [Parameter] public required string SetId { get; set; }
    [Parameter] public bool Collected { get; set; }

    private async Task ToggleOwnership()
    {
        Collected = !Collected;

        if (Collected)
        {
            await CardService.AddOwnedCardAsync(SetId, PokedexId, CancellationToken.None);
        }
        else
        {
            await CardService.RemoveOwnedCardAsync(SetId, PokedexId, CancellationToken.None);
        }
    }

    private string CardImageCssClass => Collected
        ? "pokemon-card-image"
        : "pokemon-card-image greyscale";
}