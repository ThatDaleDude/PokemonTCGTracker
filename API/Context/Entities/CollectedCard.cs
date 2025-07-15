namespace API.Context.Entities;

public class CollectedCard
{
    public Guid Id { get; init; }
    public string SetId { get; init; }
    public int PokedexId { get; init; }
    public DateOnly DateAdded { get; init; }
}