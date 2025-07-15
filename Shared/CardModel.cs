namespace Shared;

public class CardModel
{
    public int PokedexId { get; init; }
    public string Name { get; init; } = "";
    public Uri? Image { get; init; }
    public bool Collected { get; set; }
}