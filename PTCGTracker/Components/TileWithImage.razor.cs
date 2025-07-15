using Microsoft.AspNetCore.Components;

namespace PTCGTracker.Components;

public partial class TileWithImage : ComponentBase
{
    [Parameter] public required string Id { get; set; }
    [Parameter] public required string Name { get; set; }
    [Parameter] public required Uri ImageUrl { get; set; }
    [Parameter] public string? Link { get; set; }
    [Parameter] public bool IsFavourite { get; set; }
}