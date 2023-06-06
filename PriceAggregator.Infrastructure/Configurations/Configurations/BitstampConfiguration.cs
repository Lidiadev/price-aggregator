using PriceAggregator.Infrastructure.Extensions;

namespace PriceAggregator.Infrastructure.Configurations.Configurations;


public class BitstampConfiguration : ExternalServiceConfiguration
{
    public override string HttpClientName => "BitstampHttpClient"; 
    
    public string InstrumentPriceEndpointUri(string instrument, int step, int limit, DateTime start) =>
        string.Format(InstrumentPriceEndpoint, instrument, step, limit, start.ToSecondsUnixTimestamp());
}