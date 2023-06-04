namespace PriceAggregator.API.Extensions;

public static class TimeExtensions
{
    public static long ToSecondsUnixTimestamp(this DateTime dateTime) 
        => new DateTimeOffset(dateTime).ToUnixTimeSeconds();

    public static long ToMillisecondsUnixTimestamp(this DateTime dateTime) 
        => new DateTimeOffset(dateTime).ToUnixTimeMilliseconds();

    public static DateTime FormatToHourAccuracy(this DateTime dateTime) 
        => dateTime.Date.AddHours(dateTime.Hour);
}