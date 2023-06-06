using Microsoft.EntityFrameworkCore;
using PriceAggregator.Domain.Entities;
using PriceAggregator.Domain.Repository;

namespace PriceAggregator.Infrastructure.Repository;

public class PriceRepository : IPriceRepository
{
    private readonly FinancialInstrumentsDbContext _dbContext;

    public PriceRepository(FinancialInstrumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> GetPrice(string instrument, DateTime time)
    {
        var instrumentLowerName = instrument.ToLower();
        
        return await _dbContext.Prices
            .Where(p => p.Instrument.Symbol == instrumentLowerName && p.Time == time)
            .Select(p => p.Price)
            .FirstOrDefaultAsync();
    }

    public async Task<IReadOnlyList<PriceAggregate>> GetPrices(string instrument, DateTime startTime, DateTime endTime)
    {
        var instrumentLowerName = instrument.ToLower();
        
        return await _dbContext.Prices
            .Where(p => p.Instrument.Symbol == instrumentLowerName && p.Time >= startTime && p.Time <= endTime)
            .ToListAsync();
    }

    public async Task SavePrice(PriceAggregate price)
    {
        _dbContext.Prices.Add(price);

        await _dbContext.SaveChangesAsync();
    }
}