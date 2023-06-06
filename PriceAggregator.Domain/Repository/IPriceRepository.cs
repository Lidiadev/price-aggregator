using PriceAggregator.Domain.Entities;

namespace PriceAggregator.Domain.Repository;

public interface IPriceRepository
{
    Task<double> GetPrice(string instrument, DateTime time);
    
    Task<IReadOnlyList<PriceAggregate>> GetPrices(string instrument, DateTime startTime, DateTime endTime);
    
    Task SavePrice(PriceAggregate price);
}