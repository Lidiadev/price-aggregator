using PriceAggregator.API.Extensions;

namespace PriceAggregator.API.Configurations;

public class BitfinexConfiguration : ExternalServiceConfiguration
{
    public string InstrumentPriceEndpointUri(string instrument, string step, int limit, DateTime start)
    {
        var end = start.AddHours(1).FormatToHourAccuracy();
        
        return string.Format(InstrumentPriceEndpoint, step, instrument.ToUpper(), start.ToMillisecondsUnixTimestamp(), end.ToMillisecondsUnixTimestamp(), limit);
    }

    public override string HttpClientName => "BitfinexHttpClient";
}