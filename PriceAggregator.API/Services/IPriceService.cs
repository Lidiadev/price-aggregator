using PriceAggregator.API.Models;

namespace PriceAggregator.API.Services;

public interface IPriceService
{
    Task<decimal> GetAggregatedPrice(string instrument, DateTime time);
    
    Task<List<AggregatedPriceModel>> GetPersistedPrices(string instrument, DateTime start, DateTime end);
}