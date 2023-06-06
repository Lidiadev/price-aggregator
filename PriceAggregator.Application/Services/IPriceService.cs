using PriceAggregator.Application.Dto;

namespace PriceAggregator.Application.Services;

public interface IPriceService
{
    Task<AggregatedPriceModel> GetAggregatedPrice(string instrument, DateTime time);
    
    Task<List<AggregatedPriceModel>> GetPersistedPrices(string instrument, DateTime start, DateTime end);
}