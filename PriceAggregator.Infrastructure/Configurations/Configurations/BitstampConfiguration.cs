using PriceAggregator.Infrastructure.Extensions;
using PriceAggregator.Infrastructure.Mappings;

namespace PriceAggregator.Infrastructure.Configurations.Configurations;


public class BitstampConfiguration : ExternalServiceConfiguration
{
    public override string ClientName => HttpClientName.BitstampHttpClient; 
    
    public string InstrumentPriceEndpointUri(string instrument, int step, int limit, DateTime start) =>
        string.Format(InstrumentPriceEndpoint, instrument, step, limit, start.ToSecondsUnixTimestamp());
}