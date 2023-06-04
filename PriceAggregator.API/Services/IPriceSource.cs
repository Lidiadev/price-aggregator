using PriceAggregator.API.Services.External;

namespace PriceAggregator.API.Services;

public interface IPriceSource
{
    Task<PriceSourceResponse> GetPrice(string instrument, DateTime time);
}