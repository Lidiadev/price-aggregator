using PriceAggregator.API.Models;
using PriceAggregator.API.Repository;

namespace PriceAggregator.IntegrationTests;

public static class TestDataSeeder
{
    public static void PopulateDatabase(FinancialInstrumentsDbContext dbContext)
    {
        var prices = new[]
        {
            new PriceData { Time = new DateTime(2023, 6, 1, 4, 0, 0), Price = 100 },
            new PriceData { Time = new DateTime(2023, 6, 1, 5, 0, 0), Price = 110 },
            new PriceData { Time = new DateTime(2023, 6, 1, 12, 0, 0), Price = 120 },
            new PriceData { Time = new DateTime(2023, 6, 1, 13, 0, 0), Price = 130 },
            new PriceData { Time = new DateTime(2023, 6, 1, 14, 0, 0), Price = 140 },
        };

        dbContext.Prices.AddRange(prices);
        dbContext.SaveChanges();
    }
}