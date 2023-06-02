using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace PriceAggregator.IntegrationTests;

public class PriceControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    
    public PriceControllerTests(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    [Fact]
    public async Task GivenTheRequestIsValid_WhenGettingTheAggregatedPrice_TheItShouldReturnThePrice()
    {
        // Arrange
        var client = _factory.CreateClient();
        var time = new DateTime(2023, 6, 1, 12, 0, 0);

        // Act
        var response = await client.GetAsync($"/api/prices?financialInstrument=test&time={time:yyyy-MM-ddTHH:mm:ss}");

        // Assert
        response.EnsureSuccessStatusCode();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var price = JsonConvert.DeserializeObject<double>(await response.Content.ReadAsStringAsync());

        price.Should().NotBe(0);
    }
}