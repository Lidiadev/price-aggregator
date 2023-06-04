namespace PriceAggregator.API.Extensions;

public static class TimeExtensions
{
    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        var unixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
        
        return (long)(dateTime.ToUniversalTime() - unixEpoch).TotalSeconds;
    }
}