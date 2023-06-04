using Microsoft.EntityFrameworkCore;
using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public class PriceRepository : IPriceRepository
{
    private readonly FinancialInstrumentsDbContext _dbContext;

    public PriceRepository(FinancialInstrumentsDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<double> GetPrice(string instrument, DateTime time)
        => await _dbContext.Prices
            .Where(p => p.Instrument == instrument && p.Time == time)
            .Select(p => p.Price)
            .FirstOrDefaultAsync();

    public async Task<IReadOnlyList<PriceData>> GetPrices(string instrument, DateTime startTime, DateTime endTime)
    {
        var instrumentLowerName = instrument.ToLower();
        
        return await _dbContext.Prices
            .Where(p => p.Instrument == instrumentLowerName && p.Time >= startTime && p.Time <= endTime)
            .ToListAsync();
    }

    public async Task SavePrice(PriceData price)
    {
        _dbContext.Prices.Add(price);

        await _dbContext.SaveChangesAsync();
    }
}