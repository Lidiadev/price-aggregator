using PriceAggregator.Application.Dto;
using PriceAggregator.Domain.Entities;
using PriceAggregator.Domain.Repository;
using PriceAggregator.Infrastructure.Extensions;
using PriceAggregator.Infrastructure.ExternalDependencies;

namespace PriceAggregator.Application.Services;

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
    
    public async Task<AggregatedPriceModel> GetAggregatedPrice(string instrument, DateTime time)
    {
        time = time.FormatToHourAccuracy();
        var savedPrice = await _priceRepository.GetPrice(instrument, time);

        if (savedPrice != 0)
        {
            return new AggregatedPriceModel { AggregatedPrice = savedPrice };
        }
        
        var aggregatedPrice = await _priceAggregator.AggregatePrice(_priceSources, instrument, time);

        if (aggregatedPrice != 0)
        {
            var newPrice = new PriceAggregate(instrument, time, aggregatedPrice);
            await _priceRepository.SavePrice(newPrice);
        }
        
        return new AggregatedPriceModel { AggregatedPrice = aggregatedPrice };
    }

    public async Task<List<AggregatedPriceModel>> GetPersistedPrices(
        string instrument, 
        DateTime start,
        DateTime end)
    {
        end = end.FormatToHourAccuracy();
        
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