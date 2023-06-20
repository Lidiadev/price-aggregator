using System.Net;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using PriceAggregator.API.Controllers;
using PriceAggregator.Application.Dto;
using PriceAggregator.Application.Services;

namespace PriceAggregator.API.UnitTests.Controllers;

public class PricesControllerTests
{
    private readonly Mock<IPriceService> _priceServiceMock;
    private readonly Mock<ILogger<PricesController>> _loggerMock;
    private readonly PricesController _pricesController;

    private const string Instrument = "btcusd";

    public PricesControllerTests()
    {
        _priceServiceMock = new Mock<IPriceService>();
        _loggerMock = new Mock<ILogger<PricesController>>();
        _pricesController = new PricesController(_priceServiceMock.Object, _loggerMock.Object);
    }

    [Fact]
    public async Task GivenTheRequestIsValid_WhenGettingTheAggregatedPrice_ThenItShouldReturnOkWithAggregatedPrice()
    {
        // Arrange
        var time = new DateTime(2023, 6, 2, 10, 0, 0);
        const double aggregatedPrice = 100;

        _priceServiceMock
            .Setup(service => service.GetAggregatedPrice(Instrument, time))
            .ReturnsAsync(new AggregatedPriceModel { AggregatedPrice = aggregatedPrice });

        // Act
        var result = await _pricesController.GetAggregatedPrice(Instrument, time) as OkObjectResult;

        // Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        result.Value.Should().Be(aggregatedPrice);
        _priceServiceMock.Verify(service => service.GetAggregatedPrice(Instrument, time), Times.Once);
    }

    [Fact]
    public async Task GivenAnExceptionOccurs_WhenGettingTheAggregatedPrice_ThenItShouldReturnInternalServerError()
    {
        // Arrange
        var time = new DateTime(2023, 6, 2, 10, 0, 0);
        var exception = new Exception("An error occurred.");
    
        _priceServiceMock
            .Setup(service => service.GetAggregatedPrice(Instrument, time))
            .ThrowsAsync(exception);
    
        // Act
        var result = await _pricesController.GetAggregatedPrice(Instrument, time) as StatusCodeResult;
    
        // Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        _priceServiceMock.Verify(service => service.GetAggregatedPrice(Instrument, time), Times.Once);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((@object, @type) => @object!.ToString()!.Contains(exception.Message)),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }

    [Fact]
    public async Task GivenTheRequestIsValid_GettingThePersistedPrices_ThenItShouldReturnOkWithPersistedPrices()
    {
        // Arrange
        var startTime = new DateTime(2023, 6, 2, 10, 0, 0);
        var endTime = new DateTime(2023, 6, 3, 10, 0, 0);
        var prices = new List<AggregatedPriceModel>
        {
            new() { AggregatedPrice = 100, Time = startTime },
            new() { AggregatedPrice = 150, Time = endTime }
        };
    
        _priceServiceMock
            .Setup(service => service.GetPersistedPrices(Instrument, startTime, endTime))
            .ReturnsAsync(prices);
    
        // Act
        var result = await _pricesController.GetPersistedPrices(Instrument, startTime, endTime) as OkObjectResult;
    
        // Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.OK);
        result.Value.Should().BeEquivalentTo(prices);
        _priceServiceMock.Verify(service => service.GetPersistedPrices(Instrument, startTime, endTime), Times.Once);
    }
    
    [Fact]
    public async Task GivenAnExceptionOccurred_WhenGettingPersistedPrices_ThenItShouldReturnsInternalServerError()
    {
        // Arrange
        var startTime = new DateTime(2023, 6, 2, 10, 0, 0);
        var endTime = new DateTime(2023, 6, 3, 10, 0, 0);
        var exception = new Exception("An error occurred.");
    
        _priceServiceMock
            .Setup(service => service.GetPersistedPrices(Instrument, startTime, endTime))
            .ThrowsAsync(exception);
    
        // Act
        var result = await _pricesController.GetPersistedPrices(Instrument, startTime, endTime) as StatusCodeResult;
    
        // Assert
        result!.StatusCode.Should().Be((int)HttpStatusCode.InternalServerError);
        _priceServiceMock.Verify(service => service.GetPersistedPrices(Instrument, startTime, endTime), Times.Once);
        _loggerMock.Verify(
            x => x.Log(
                LogLevel.Error,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((@object, @type) => @object!.ToString()!.Contains(exception.Message)),
                exception,
                It.IsAny<Func<It.IsAnyType, Exception, string>>()),
            Times.Once);
    }
}