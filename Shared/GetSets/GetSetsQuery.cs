namespace Shared.GetSets;

public class GetSetsQuery
{
    public string? SearchText { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
}