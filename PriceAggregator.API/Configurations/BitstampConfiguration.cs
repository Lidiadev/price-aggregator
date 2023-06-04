using PriceAggregator.API.Extensions;

namespace PriceAggregator.API.Configurations;

public class BitstampConfiguration
{
    public string ServiceBaseUrl { get; set; }

    public string InstrumentPriceEndpoint { get; set; }
    
    public bool IsEnabled { get; set; }
    
    public Uri BaseUri => new(ServiceBaseUrl);

    public string InstrumentPriceEndpointUri(string financialInstrument, int step, int limit, DateTime start) =>
        string.Format(InstrumentPriceEndpoint, financialInstrument, step, limit, start.ToUnixTimestamp());
}