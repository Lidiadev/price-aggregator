namespace PriceAggregator.API.Services;

public interface IPriceAggregator
{
    Task<double> AggregatePrice(IReadOnlyList<IPriceSource> priceSources, string financialInstrument, DateTime time);
}