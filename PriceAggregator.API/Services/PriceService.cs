using PriceAggregator.API.Models;
using PriceAggregator.API.Repository;

namespace PriceAggregator.API.Services;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IPriceAggregatorService _priceAggregatorService;

    public PriceService(
        IPriceRepository priceRepository, 
        IPriceAggregatorService priceAggregatorService)
    {
        _priceRepository = priceRepository;
        _priceAggregatorService = priceAggregatorService;
    }
    
    public async Task<double> GetAggregatedPrice(string financialInstrument, DateTime time)
    {
        var aggregatedPrice = await _priceAggregatorService.AggregatePrice(financialInstrument, time);
        
        var newPrice = new PriceModel { Time = time, Price = aggregatedPrice };
        await _priceRepository.SavePrice(newPrice);

        return aggregatedPrice;
    }

    public Task<List<AggregatedPriceModel>> GetPersistedPrices(string financialInstrument, DateTime start, DateTime end)
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