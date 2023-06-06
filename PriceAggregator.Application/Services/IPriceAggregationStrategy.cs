namespace PriceAggregator.Application.Services;

public interface IPriceAggregationStrategy
{
    double AggregatePrices(IReadOnlyList<double> prices);
}