namespace PriceAggregator.API.Services;

public interface IPriceAggregator
{
    Task<decimal> AggregatePrice(IReadOnlyList<IPriceSource> priceSources, string financialInstrument, DateTime time);
}