namespace PriceAggregator.Infrastructure.ExternalDependencies;

public interface IPriceSource
{
    Task<PriceSourceResponse> GetPrice(string instrument, DateTime time);
}