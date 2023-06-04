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

    public async Task<decimal> GetPrice(DateTime time)
        => await _dbContext.Prices
            .Where(p => p.Time == time)
            .Select(p => p.Price)
            .FirstOrDefaultAsync();

        public async Task<IReadOnlyList<PriceData>> GetPrices(DateTime startTime, DateTime endTime)
        => await _dbContext.Prices
            .Where(p => p.Time >= startTime && p.Time <= endTime)
            .ToListAsync();

    public async Task SavePrice(PriceData price)
    {
        _dbContext.Prices.Add(price);

        await _dbContext.SaveChangesAsync();
    }
}