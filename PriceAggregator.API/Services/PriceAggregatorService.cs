namespace PriceAggregator.API.Services;

public class PriceAggregatorService : IPriceAggregatorService
{
    public Task<double> AggregatePrice(string financialInstrument, DateTime time)
    {
        return Task.FromResult(10.0);
    }
}