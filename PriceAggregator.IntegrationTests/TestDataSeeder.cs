using PriceAggregator.API.Models;
using PriceAggregator.API.Repository;

namespace PriceAggregator.IntegrationTests;

public static class TestDataSeeder
{
    public static void PopulateDatabase(FinancialInstrumentsDbContext dbContext)
    {
        var prices = new[]
        {
            new PriceData ("btcusd", new DateTime(2023, 6, 1, 4, 0, 0),  100),
            new PriceData ("btcusd", new DateTime(2023, 6, 1, 5, 0, 0),  110),
            new PriceData ("btcusd", new DateTime(2023, 6, 1, 12, 0, 0),  120),
            new PriceData ("btcusd", new DateTime(2023, 6, 1, 12, 0, 0),  130),
            new PriceData ("btceur", new DateTime(2023, 6, 1, 4, 0, 0),  140),
            new PriceData ("btceur", new DateTime(2023, 6, 1, 4, 0, 0),  150),
        };

        dbContext.Prices.AddRange(prices);
        dbContext.SaveChanges();
    }
}