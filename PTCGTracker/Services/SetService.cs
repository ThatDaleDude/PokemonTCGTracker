using System.Net.Http.Json;
using PTCGTracker.Infrastructure.Extensions;
using Shared;
using Shared.GetSets;

namespace PTCGTracker.Services;

public interface ISetService
{
    public Task<GetSetsResponse> GetAsync(GetSetsQuery query, CancellationToken cancellationToken);
    public Task<List<CardModel>> GetCardsAsync(string setId, CancellationToken cancellationToken);
    public Task AddSetAsFavouriteAsync(string setId, CancellationToken cancellationToken);
    public Task RemoveSetAsFavouriteAsync(string setId, CancellationToken cancellationToken);
}

public class SetService(HttpClient client) : ISetService
{
    public async Task<GetSetsResponse> GetAsync(GetSetsQuery query, CancellationToken cancellationToken) =>
        await client.GetFromJsonAsync<GetSetsResponse>($"Sets?{query.ToQueryString()}", cancellationToken) ?? new GetSetsResponse();

    public async Task<List<CardModel>> GetCardsAsync(string setId, CancellationToken cancellationToken) =>
        await client.GetFromJsonAsync<List<CardModel>>($"Sets/{setId}", cancellationToken) ?? [];

    public async Task AddSetAsFavouriteAsync(string setId, CancellationToken cancellationToken) =>
        await client.PostAsync($"Sets/{setId}/favourite", null, cancellationToken);
    
    public async Task RemoveSetAsFavouriteAsync(string setId, CancellationToken cancellationToken) =>
        await client.DeleteAsync($"Sets/{setId}/favourite", cancellationToken);
}