namespace PriceAggregator.API.Services;

public interface IPriceAggregationStrategy
{
    decimal AggregatePrices(IReadOnlyList<decimal> prices);
}