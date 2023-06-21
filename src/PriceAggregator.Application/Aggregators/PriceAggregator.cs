using PriceAggregator.Application.Aggregators;
using PriceAggregator.Application.Dto;

namespace PriceAggregator.Application.Services;

public class PriceAggregator : IPriceAggregator
{
    private readonly IPriceAggregationStrategy _aggregationStrategy;

    public PriceAggregator(IPriceAggregationStrategy aggregationStrategy)
    {
        _aggregationStrategy = aggregationStrategy;
    }
    
    public async Task<double> AggregatePrice(IReadOnlyList<IPriceSource> priceSources, string financialInstrument, DateTime time)
    {
        var priceTasks = new List<Task<PriceSourceResponse>>();
        
        foreach (var priceSource in priceSources)
        {
            var priceTask = priceSource.GetPrice(financialInstrument, time);
            priceTasks.Add(priceTask);
        }

        var priceResponses = await Task.WhenAll(priceTasks);

        var prices = priceResponses
            .Where(p => p.IsSuccessful)
            .Select(p => p.Price)
            .ToList();
        
        return _aggregationStrategy.AggregatePrices(prices);
    }
}