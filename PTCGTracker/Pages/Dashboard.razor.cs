using Microsoft.AspNetCore.Components;
using Shared.GetSets;

namespace PTCGTracker.Pages;

public partial class Dashboard : ComponentBase
{
    private const int PageSize = 12;
    private List<SetViewModel> Sets { get; set; } = [];
    private List<SetViewModel> FilteredSets => GetFilteredSets();
    private string? SearchText { get; set; }
    private int CurrentPage { get; set; } = 1;
    private int TotalSets { get; set; }
    private bool FavouriteSetsFilter { get; set; }
    
    protected override async Task OnInitializedAsync()
    {
        await LoadSetsAsync();
    }

    private List<SetViewModel> GetFilteredSets()
    {
        // TODO: This doesn't work as expected. It's filtering for the favourites on the current page only (because that's what's been returned from the API)
        // When favourites is toggled on, we need to make another API request and only get sets based on the set ids in the favourite sets table
        
        var filteredSets = Sets;

        if (FavouriteSetsFilter)
        {
            filteredSets = filteredSets.Where(x => x.IsFavourite).ToList();
        }

        return filteredSets;
    }
    
    private async Task OnSearchChangedAsync()
    {
        CurrentPage = 1;
        await LoadSetsAsync();
    }

    private async Task LoadSetsAsync()
    {
        var query = new GetSetsQuery
        {
            SearchText  = SearchText,
            CurrentPage = CurrentPage,
            PageSize    = PageSize
        };
        var response = await SetService.GetAsync(query, CancellationToken.None);
        
        Sets = response.Sets
            .Select(x =>
            {
                var queryParams = new Dictionary<string, object?>
                {
                    ["SetId"]   = x.ExternalId,
                    ["SetName"] = x.Name,
                    ["IsFavourite"] = x.IsFavourite
                };
                
                var uri = Navigation.GetUriWithQueryParameters("/Set", queryParams);
                
                return new SetViewModel
                {
                    Name     = x.Name,
                    ImageUrl = x.Logo!,
                    Link     = uri,
                    IsFavourite = x.IsFavourite
                };
            }).ToList();

        TotalSets = response.TotalSets;
    }
}

public class SetViewModel
{
    public required string Name { get; init; }
    public required Uri ImageUrl { get; init; }
    public required string Link { get; init; }
    public bool IsFavourite { get; init; }
}