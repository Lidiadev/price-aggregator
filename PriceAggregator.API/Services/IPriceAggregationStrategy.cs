namespace PriceAggregator.API.Services;

public interface IPriceAggregationStrategy
{
    double AggregatePrices(IReadOnlyList<double> prices);
}