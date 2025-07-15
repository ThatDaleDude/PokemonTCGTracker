namespace PTCGTracker.Services;

public interface ICardService
{
    public Task AddOwnedCardAsync(string setId, int pokedexId, CancellationToken cancellationToken);
    public Task RemoveOwnedCardAsync(string setId, int pokedexId, CancellationToken cancellationToken);
}

public class CardService(HttpClient client) : ICardService
{
    public async Task AddOwnedCardAsync(string setId, int pokedexId, CancellationToken cancellationToken) =>
        await client.PostAsync($"Sets/{setId}/{pokedexId}", null, cancellationToken: cancellationToken);

    public async Task RemoveOwnedCardAsync(string setId, int pokedexId, CancellationToken cancellationToken) =>
        await client.DeleteAsync($"Sets/{setId}/{pokedexId}", cancellationToken);
}