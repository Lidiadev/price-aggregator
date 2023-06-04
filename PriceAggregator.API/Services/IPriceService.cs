using PriceAggregator.API.Models;

namespace PriceAggregator.API.Services;

public interface IPriceService
{
    Task<decimal> GetAggregatedPrice(string financialInstrument, DateTime time);
    
    Task<List<AggregatedPriceModel>> GetPersistedPrices(string financialInstrument, DateTime start, DateTime end);
}