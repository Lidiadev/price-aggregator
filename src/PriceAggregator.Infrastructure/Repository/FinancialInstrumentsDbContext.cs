using Microsoft.EntityFrameworkCore;
using PriceAggregator.Domain.Entities;
using PriceAggregator.Infrastructure.Repository.Configurations;

namespace PriceAggregator.Infrastructure.Repository;

public class FinancialInstrumentsDbContext : DbContext
{
    public DbSet<PriceAggregate> Prices { get; set; }

    public FinancialInstrumentsDbContext(DbContextOptions<FinancialInstrumentsDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new PriceConfiguration());
    }
}