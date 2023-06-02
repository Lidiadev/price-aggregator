using Microsoft.EntityFrameworkCore;
using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public class FinancialInstrumentsDbContext : DbContext
{
    public DbSet<PriceData> Prices { get; set; }

    public FinancialInstrumentsDbContext(DbContextOptions<FinancialInstrumentsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    }
}