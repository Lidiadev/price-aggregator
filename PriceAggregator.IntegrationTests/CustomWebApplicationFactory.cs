using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PriceAggregator.API.Repository;

namespace PriceAggregator.IntegrationTests;

public class CustomWebApplicationFactory<TProgram>
    : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureServices(services =>
        {
            builder.ConfigureAppConfiguration((context, config) =>
            {
                var assemblyPath = Assembly.GetExecutingAssembly().Location;
                var appsettingsPath = Path.Combine(Path.GetDirectoryName(assemblyPath), "appsettings.json");
                config.AddJsonFile(appsettingsPath);
            });
                
            builder.ConfigureServices(services =>
            {
                var dbContextOptions = new DbContextOptionsBuilder<FinancialInstrumentsDbContext>()
                    .UseInMemoryDatabase(databaseName: "TestDb")
                    .Options;

                services.AddSingleton(dbContextOptions);
            });
        });

        builder.UseEnvironment("IntegrationTest");
    }
}