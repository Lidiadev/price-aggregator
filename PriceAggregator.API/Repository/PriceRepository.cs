using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public class PriceRepository : IPriceRepository
{
    public Task SavePrice(PriceModel newPrice)
    {
        return Task.FromResult(true);
    }
}