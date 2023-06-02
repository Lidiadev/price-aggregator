using PriceAggregator.API.Models;

namespace PriceAggregator.API.Services;

public interface IPriceService
{
    Task<double> GetAggregatedPrice(string financialInstrument, DateTime time);
    
    Task<List<AggregatedPriceModel>> GetPersistedPrices(string financialInstrument, DateTime start, DateTime end);
}