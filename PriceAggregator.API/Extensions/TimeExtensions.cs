namespace PriceAggregator.API.Extensions;

public static class TimeExtensions
{
    public static long ToUnixTimestamp(this DateTime dateTime)
    {
        // var unixEpoch = new DateTime(1970, 1, 14, 0, 0, 0, DateTimeKind.Utc);
        //
        // return (long)(dateTime.ToUniversalTime() - unixEpoch).TotalMilliseconds;


        return (long)dateTime.Subtract(DateTime.UnixEpoch).TotalSeconds;
    }
    
    public static DateTime FormatToHourAccuracy(this DateTime dateTime) 
        => dateTime.Date.AddHours(dateTime.Hour);
}