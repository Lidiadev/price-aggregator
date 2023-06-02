using PriceAggregator.API.Models;

namespace PriceAggregator.API.Services;

public interface IPriceService
{
    Task<double> GetAggregatedPrice(DateTime time);
    Task<List<AggregatedPriceModel>> GetPersistedPrices(DateTime start, DateTime end);
}