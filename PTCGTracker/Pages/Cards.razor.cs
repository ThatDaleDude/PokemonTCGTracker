using Microsoft.AspNetCore.Components;
using MudBlazor;
using Shared;

namespace PTCGTracker.Pages;

public partial class Cards : ComponentBase
{
    [Parameter]
    [SupplyParameterFromQuery]
    public required string SetId { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery]
    public required string SetName { get; set; }
    
    [Parameter]
    [SupplyParameterFromQuery]
    public bool IsFavourite { get; set; }
    private List<CardModel> CardModels { get; set; } = [];
    private string? SearchText { get; set; }
    private bool MissingCardsFilter { get; set; }

    private List<CardModel> FilteredCards => GetFilteredCards();

    protected override async Task OnParametersSetAsync()
    {
        await base.OnParametersSetAsync();
        
        CardModels = await SetService.GetCardsAsync(SetId, CancellationToken.None);
    }

    private List<CardModel> GetFilteredCards()
    {
        var filteredCards = CardModels;

        if (!string.IsNullOrWhiteSpace(SearchText))
        {
            filteredCards = filteredCards.Where(x => x.Name.Contains(SearchText, StringComparison.CurrentCultureIgnoreCase)).ToList();
        }

        if (MissingCardsFilter)
        {
            filteredCards = filteredCards.Where(x => !x.Collected).ToList();
        }
        
        return filteredCards;
    }
    
    private async Task ToggleFavourite()
    {
        IsFavourite = !IsFavourite;

        if (IsFavourite)
        {
            await SetService.AddSetAsFavouriteAsync(SetId, CancellationToken.None);
        }
        else
        {
            await SetService.RemoveSetAsFavouriteAsync(SetId, CancellationToken.None);
        }
    }

    private string GetStarIcon() => IsFavourite ? Icons.Material.Filled.Star : Icons.Material.Filled.StarBorder;
}