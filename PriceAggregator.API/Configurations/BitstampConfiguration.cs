using PriceAggregator.API.Extensions;

namespace PriceAggregator.API.Configurations;

public class BitstampConfiguration : ExternalServiceConfiguration
{
    public string InstrumentPriceEndpointUri(string financialInstrument, int step, int limit, DateTime start) =>
        string.Format(InstrumentPriceEndpoint, financialInstrument, step, limit, start.ToUnixTimestamp());
}