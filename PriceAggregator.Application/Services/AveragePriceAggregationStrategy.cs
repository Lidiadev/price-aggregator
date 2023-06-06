namespace PriceAggregator.Application.Services;

public class PriceAverageAggregationStrategy : IPriceAggregationStrategy
{
    private const double DefaultPrice = 0;
    
    public double AggregatePrices(IReadOnlyList<double> prices) =>
        prices.Count > 0 
            ? prices.Average() 
            : DefaultPrice;
}