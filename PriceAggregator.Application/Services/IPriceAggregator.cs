using PriceAggregator.Infrastructure.ExternalDependencies;

namespace PriceAggregator.Application.Services;

public interface IPriceAggregator
{
    Task<double> AggregatePrice(IReadOnlyList<IPriceSource> priceSources, string financialInstrument, DateTime time);
}