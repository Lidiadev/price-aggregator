using PriceAggregator.API.Models;
using PriceAggregator.API.Repository;

namespace PriceAggregator.API.Services;

public class PriceService : IPriceService
{
    private readonly IPriceRepository _priceRepository;
    private readonly IPriceAggregator _priceAggregator;
    private readonly IReadOnlyList<IPriceSource> _priceSources;

    public PriceService(
        IPriceRepository priceRepository, 
        IPriceAggregator priceAggregator,
        IEnumerable<IPriceSource> priceSources)
    {
        _priceRepository = priceRepository;
        _priceAggregator = priceAggregator;
        _priceSources = priceSources.ToList();
    }
    
    public async Task<double> GetAggregatedPrice(string instrument, DateTime time)
    {
        var aggregatedPrice = await _priceAggregator.AggregatePrice(_priceSources, instrument, time);
        
        var newPrice = new PriceData(instrument, time, aggregatedPrice);
        await _priceRepository.SavePrice(newPrice);

        return aggregatedPrice;
    }

    public async Task<List<AggregatedPriceModel>> GetPersistedPrices(
        string instrument, 
        DateTime start,
        DateTime end)
    {
        var prices = await _priceRepository.GetPrices(instrument, start, end);

        return prices
            .Select(p => new AggregatedPriceModel
            {
                AggregatedPrice = p.Price,
                Time = p.Time
            })
            .ToList();
    }
}