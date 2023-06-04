using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public interface IPriceRepository
{
    Task<double> GetPrice(string instrument, DateTime time);
    
    Task<IReadOnlyList<PriceData>> GetPrices(string instrument, DateTime startTime, DateTime endTime);
    
    Task SavePrice(PriceData price);
}