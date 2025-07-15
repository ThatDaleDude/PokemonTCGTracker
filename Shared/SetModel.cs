namespace Shared;

public class SetModel
{
    public string Name { get; init; } = "";
    public string ExternalId { get; init; } = "";
    public Uri? Symbol { get; init; }
    public Uri? Logo { get; init; }
    public bool IsFavourite { get; init; }
}