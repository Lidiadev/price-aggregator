using PriceAggregator.Application.Services;

namespace PriceAggregator.Application.Aggregators;

public interface IPriceAggregator
{
    Task<double> AggregatePrice(IReadOnlyList<IPriceSource> priceSources, string financialInstrument, DateTime time);
}