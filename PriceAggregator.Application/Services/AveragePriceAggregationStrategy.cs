namespace PriceAggregator.Application.Services;

public class PriceAverageAggregationStrategy : IPriceAggregationStrategy
{
    public double AggregatePrices(IReadOnlyList<double> prices) =>
        prices.Count > 0 
            ? prices.Average() 
            : 0;
}