namespace PriceAggregator.API.Services;

public class PriceAverageAggregationStrategy : IPriceAggregationStrategy
{
    public decimal AggregatePrices(IReadOnlyList<decimal> prices) =>
        prices.Count > 0 
            ? prices.Average() 
            : 0;
}