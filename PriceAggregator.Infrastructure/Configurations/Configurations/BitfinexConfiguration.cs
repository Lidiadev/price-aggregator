
using PriceAggregator.Infrastructure.Extensions;
using PriceAggregator.Infrastructure.Mappings;

namespace PriceAggregator.Infrastructure.Configurations.Configurations;

public class BitfinexConfiguration : ExternalServiceConfiguration
{
    public override string ClientName => HttpClientName.BitfinexHttpClient;
    
    public string InstrumentPriceEndpointUri(string instrument, string step, int limit, DateTime start)
    {
        var end = start.AddHours(1).FormatToHourAccuracy();
        
        return string.Format(InstrumentPriceEndpoint, step, instrument.ToUpper(), start.ToMillisecondsUnixTimestamp(), end.ToMillisecondsUnixTimestamp(), limit);
    }
}