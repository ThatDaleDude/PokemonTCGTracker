namespace Shared.GetSets;

public class GetSetsQuery
{
    public string? SearchText { get; init; }
    public int CurrentPage { get; init; } = 1;
    public int PageSize { get; init; } = 12;
}