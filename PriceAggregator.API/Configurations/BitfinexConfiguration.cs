using PriceAggregator.API.Extensions;

namespace PriceAggregator.API.Configurations;

public class BitfinexConfiguration : ExternalServiceConfiguration
{
    public string InstrumentPriceEndpointUri(string instrument, string step, int limit, DateTime start)
    {
        var end = start.Date.AddHours(1);
        
        return string.Format(InstrumentPriceEndpoint, step, instrument, start.ToUnixTimestamp(), end.ToUnixTimestamp(), limit);
    }
}