using PriceAggregator.API.Models;

namespace PriceAggregator.API.Services;

public class PriceService : IPriceService
{
    public Task<double> GetAggregatedPrice(DateTime time)
    {
        return Task.FromResult(10.0);
    }

    public Task<List<AggregatedPriceModel>> GetPersistedPrices(DateTime start, DateTime end)
    {
        return Task.FromResult(new List<AggregatedPriceModel>
        {
            new()
            {
                AggregatedPrice = 10,
                Time = DateTime.Now
            }
        });
    }
}