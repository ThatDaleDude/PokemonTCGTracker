namespace Shared.GetSets;

public class GetSetsResponse
{
    public List<SetModel> Sets { get; init; } = [];
    public int TotalSets { get; init; }
}