namespace PriceAggregator.Application.Aggregators;

public interface IPriceAggregationStrategy
{
    double AggregatePrices(IReadOnlyList<double> prices);
}