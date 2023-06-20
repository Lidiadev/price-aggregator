using System.Net;
using System.Text.Json;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PriceAggregator.Infrastructure.Configurations.Configurations;
using PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

namespace PriceAggregator.Infrastructure.UnitTests.ExternalDependencies.Bitstamp;

public class BitstampServiceTests
{
    private readonly Mock<IOptions<BitstampConfiguration>> _bitstampConfigurationMock;
    private BitstampService _bitstampService;
    
    const string Instrument = "BTCUSD";

    public BitstampServiceTests()
    {
        _bitstampConfigurationMock = new Mock<IOptions<BitstampConfiguration>>();   
        
        _bitstampConfigurationMock
            .Setup(x => x.Value)
            .Returns(new BitstampConfiguration
            {
                InstrumentPriceEndpoint = "api/v2/ohlc/{0}?step={1}&limit={2}&start={3}",
            });
    }

    [Fact]
    public async Task GivenTheApiReturnsASuccessfulResponse_WhenGettingPrice_ThenItShouldReturnPriceSourceResponseWithSuccess()
    {
        // Arrange
        const double expectedPrice = 1234.56;
        var apiResponse = new BitstampResponse
        {
            Data = new CandleResponse
            {
                Candles = new List<CandleData>
                {
                    new() { Close = expectedPrice.ToString() },
                    new() { Close = "20" }
                }
            }
        };

        var httpClient = new HttpClient(new MockHttpMessageHandler(async request =>
        {
            request.Method.Should().Be(HttpMethod.Get);
            request.RequestUri.ToString().Should().Contain(Instrument);

            return new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent(JsonSerializer.Serialize(apiResponse))
            };
        }));
        httpClient.BaseAddress = new Uri("http://localhost:8080");

        var bitstampService = new BitstampService(httpClient, _bitstampConfigurationMock.Object, new Mock<ILogger<BitstampService>>().Object);

        // Act
        var priceSourceResponse = await bitstampService.GetPrice(Instrument, DateTime.UtcNow);

        // Assert
        priceSourceResponse.IsSuccessful.Should().BeTrue();
        priceSourceResponse.Price.Should().Be(expectedPrice);
    }
    
    [Fact]
    public async Task GivenTheApiReturnsASuccessfulResponseWithoutCandles_WhenGettingPrice_ThenItShouldReturnPriceSourceResponseWithFailure()
    {
        // Arrange
        var apiResponse = new BitstampResponse();

        var httpClient = new HttpClient(new MockHttpMessageHandler(async _ => new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(JsonSerializer.Serialize(apiResponse))
        }));
        httpClient.BaseAddress = new Uri("http://localhost:8080");

        var bitstampService = new BitstampService(httpClient, _bitstampConfigurationMock.Object, new Mock<ILogger<BitstampService>>().Object);

        // Act
        var priceSourceResponse = await bitstampService.GetPrice(Instrument, DateTime.UtcNow);

        // Assert
        priceSourceResponse.IsSuccessful.Should().BeFalse();
        priceSourceResponse.Price.Should().Be(0);
    }
    
    [Fact]
    public async Task GivenTheApiDoesNotReturnASuccessfulResponse_WhenGettingPrice_ThenItShouldReturnPriceSourceResponseWithFailure()
    {
        // Arrange

        var httpClient = new HttpClient(new MockHttpMessageHandler(async _ => new HttpResponseMessage(HttpStatusCode.InternalServerError)));

        var bitstampService = new BitstampService(httpClient, _bitstampConfigurationMock.Object, new Mock<ILogger<BitstampService>>().Object);

        // Act
        var priceSourceResponse = await bitstampService.GetPrice(Instrument, DateTime.UtcNow);

        // Assert
        priceSourceResponse.IsSuccessful.Should().BeFalse();
        priceSourceResponse.Price.Should().Be(0);
    }

    [Fact]
    public async Task GivenTheApiThrowsAnException_WhenGettingPrice_ThenItShouldReturnPriceSourceResponseWithFailure()
    {
        // Arrange
        var httpClient = new HttpClient(new MockHttpMessageHandler(async _ => throw new Exception("An error occured")));
        var bitstampService = new BitstampService(httpClient, _bitstampConfigurationMock.Object, new Mock<ILogger<BitstampService>>().Object);

        // Act
        var priceSourceResponse = await bitstampService.GetPrice(Instrument, DateTime.UtcNow);

        // Assert
        priceSourceResponse.IsSuccessful.Should().BeFalse();
        priceSourceResponse.Price.Should().Be(0);
    }
}