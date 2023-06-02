using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public interface IPriceRepository
{
    Task<double> GetPrice(DateTime time);
    
    Task<IReadOnlyList<PriceData>> GetPrices(DateTime startTime, DateTime endTime);
    
    Task SavePrice(PriceData price);
}