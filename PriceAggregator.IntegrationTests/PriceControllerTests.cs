    using System.Net;
    using FluentAssertions;
    using Microsoft.AspNetCore.Mvc.Testing;
    using Microsoft.Extensions.DependencyInjection;
    using Newtonsoft.Json;
    using PriceAggregator.API.Models;
    using PriceAggregator.API.Repository;

    namespace PriceAggregator.IntegrationTests;

    public class PriceControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public PriceControllerTests(CustomWebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        
        [Fact]
        public async Task GivenTheRequestIsValid_WhenGettingTheAggregatedPrice_ThenItShouldReturnThePrice()
        {
            // Arrange
            var client = _factory.CreateClient();
            var time = new DateTime(2023, 1, 1, 12, 14, 15);
   
            // Act
            var response = await client.GetAsync($"/api/v1/prices/btcusd/{time:yyyy-MM-ddTHH:mm:ss}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var price = JsonConvert.DeserializeObject<double>(await response.Content.ReadAsStringAsync());
            price.Should().Be(16544);
        }
        
        [Fact]
        public async Task GivenTheExternalServicesAreReturningBadRequest_WhenGettingTheAggregatedPrice_ThenItShouldReturnTheDefaultPrice()
        {
            // Arrange
            var client = _factory.CreateClient();
            var time = new DateTime(2023, 2, 1, 12, 14, 15);
   
            // Act
            var response = await client.GetAsync($"/api/v1/prices/btcusd/{time:yyyy-MM-ddTHH:mm:ss}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var price = JsonConvert.DeserializeObject<double>(await response.Content.ReadAsStringAsync());
            price.Should().Be(0);
        }
        
        [Fact]
        public async Task GivenValidRequest_WhenGettingThePersistedPrices_ThenItShouldReturnPricesInRange()
        {
            // Arrange
            var client = _factory.CreateClient();
            var startTime = new DateTime(2023, 6, 1, 6, 0, 0); 
            var endTime = new DateTime(2023, 6, 1, 13, 59, 59);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FinancialInstrumentsDbContext>();
            TestDataSeeder.PopulateDatabase(dbContext);

            // Act
            var response = await client.GetAsync($"/api/v1/prices/persisted/btcusd?startTime={startTime:yyyy-MM-ddTHH:mm:ss}&endTime={endTime:yyyy-MM-ddTHH:mm:ss}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var prices = JsonConvert.DeserializeObject<AggregatedPriceModel[]>(await response.Content.ReadAsStringAsync());
            prices.Should().NotBeNullOrEmpty();
            prices!.Length.Should().Be(4);
        }
        
        [Fact]
        public async Task GivenTheRequestedInstrumentDoesNotExist_WhenGettingThePersistedPrices_ThenItShouldReturnPricesInRange()
        {
            // Arrange
            var client = _factory.CreateClient();
            var startTime = new DateTime(2023, 6, 1, 6, 0, 0); 
            var endTime = new DateTime(2023, 6, 1, 13, 59, 59);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FinancialInstrumentsDbContext>();
            TestDataSeeder.PopulateDatabase(dbContext);

            // Act
            var response = await client.GetAsync($"/api/v1/prices/persisted/random?startTime={startTime:yyyy-MM-ddTHH:mm:ss}&endTime={endTime:yyyy-MM-ddTHH:mm:ss}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var prices = JsonConvert.DeserializeObject<AggregatedPriceModel[]>(await response.Content.ReadAsStringAsync());
            prices!.Should().BeEmpty();
        }
        
        [Fact]
        public async Task GivenThereIsNoSavedPriceForTheRange_WhenGettingThePersistedPrices_ThenItShouldReturnPricesInRange()
        {
            // Arrange
            var client = _factory.CreateClient();
            var startTime = new DateTime(2023, 6, 1, 6, 0, 0); 
            var endTime = new DateTime(2023, 6, 1, 13, 59, 59);

            using var scope = _factory.Services.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<FinancialInstrumentsDbContext>();
            TestDataSeeder.PopulateDatabase(dbContext);

            // Act
            var response = await client.GetAsync($"/api/v1/prices/persisted/btcusd?startTime={startTime:yyyy-MM-ddTHH:mm:ss}&endTime={endTime:yyyy-MM-ddTHH:mm:ss}");

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

            var prices = JsonConvert.DeserializeObject<AggregatedPriceModel[]>(await response.Content.ReadAsStringAsync());
            prices.Should().NotBeNullOrEmpty();
            prices!.Length.Should().Be(2);
        }
    }