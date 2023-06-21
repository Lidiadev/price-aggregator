using PriceAggregator.Domain.Entities;
using PriceAggregator.Infrastructure.Repository;

namespace PriceAggregator.IntegrationTests;

public static class TestDataSeeder
{
    public static void PopulateDatabase(FinancialInstrumentsDbContext dbContext)
    {
        var prices = new[]
        {
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 4, 0, 0),  100),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 5, 0, 0),  110),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 12, 0, 0),  120),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 12, 0, 0),  130),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 14, 0, 0),  100),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 15, 0, 0),  110),
            new PriceAggregate ("btcusd", new DateTime(2023, 6, 1, 17, 0, 0),  120),
            new PriceAggregate ("btcusd", new DateTime(2023, 7, 1, 12, 0, 0),  130),
            new PriceAggregate ("btcusd", new DateTime(2023, 8, 1, 4, 0, 0),  100),
            new PriceAggregate ("btcusd", new DateTime(2023, 8, 1, 5, 0, 0),  110),
            new PriceAggregate ("btcusd", new DateTime(2023, 8, 1, 12, 0, 0),  120),
            new PriceAggregate ("btcusd", new DateTime(2023, 8, 1, 12, 0, 0),  130),
            new PriceAggregate ("btceur", new DateTime(2023, 6, 1, 4, 0, 0),  140),
            new PriceAggregate ("btceur", new DateTime(2023, 6, 1, 4, 0, 0),  150),
        };

        dbContext.Prices.AddRange(prices);
        dbContext.SaveChanges();
    }
}