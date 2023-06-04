using PriceAggregator.API.Extensions;

namespace PriceAggregator.API.Configurations;


public class BitstampConfiguration : ExternalServiceConfiguration
{
    public override string HttpClientName => "BitstampHttpClient"; 
    
    public string InstrumentPriceEndpointUri(string instrument, int step, int limit, DateTime start) =>
        string.Format(InstrumentPriceEndpoint, instrument, step, limit, start.ToSecondsUnixTimestamp());
}