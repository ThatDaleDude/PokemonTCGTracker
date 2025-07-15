namespace PTCGTracker.Infrastructure.Extensions;

public static class QueryStringExtensions
{
    public static string ToQueryString(this object obj)
    {
        var props = from p in obj.GetType().GetProperties()
            let value = p.GetValue(obj)
            where value is not null
            select $"{Uri.EscapeDataString(p.Name)}={Uri.EscapeDataString(value.ToString()!)}";

        return string.Join("&", props);
    }
}