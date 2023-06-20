using FluentAssertions;
using Moq;
using PriceAggregator.Application.Dto;
using PriceAggregator.Domain.Entities;
using PriceAggregator.Domain.Repository;
using PriceAggregator.Infrastructure.ExternalDependencies;

namespace PriceAggregator.Application.Services.UnitTests.Services;

public class PriceServiceTests
{
    private readonly Mock<IPriceRepository> _priceRepositoryMock;
    private readonly Mock<IPriceAggregator> _priceAggregatorMock;
    private readonly PriceService _priceService;
    
    private const string Instrument = "Eth";

    public PriceServiceTests()
    {
        _priceRepositoryMock = new Mock<IPriceRepository>();
        _priceAggregatorMock = new Mock<IPriceAggregator>();
        var priceSources = new List<IPriceSource> { new Mock<IPriceSource>().Object};

        _priceService = new PriceService(_priceRepositoryMock.Object, _priceAggregatorMock.Object, priceSources);
    }
    
    [Fact]
    public async Task GivenThePriceExistsInTheRepo_WhenGettingTheAggregatedPrice_ThenItShouldReturnTheSavedPrice()
    {
        // Arrange
        var time = new DateTime(2023, 6, 20, 12, 0, 0);
        const double price = 10;
        _priceRepositoryMock
            .Setup(repo => repo.GetPrice(Instrument, time))
            .ReturnsAsync(price);

        // Act
        var result = await _priceService.GetAggregatedPrice(Instrument, time);

        // Assert
        result.AggregatedPrice.Should().Be(price);
        _priceRepositoryMock.Verify(repo => repo.GetPrice(Instrument, time), Times.Once);
        _priceAggregatorMock.Verify(aggregator => aggregator.AggregatePrice(It.IsAny<IReadOnlyList<IPriceSource>>(), It.IsAny<string>(), It.IsAny<DateTime>()), Times.Never);
        _priceRepositoryMock.Verify(repo => repo.SavePrice(It.IsAny<PriceAggregate>()), Times.Never);
    }

    [Fact]
    public async Task GivenTheAggregatedPriceDoesNotExistInTheRepository_WhenGettingTheAggregatedPrice_ThenItShouldReturnTheAggregatedPriceAndSave()
    {
        // Arrange
        var time = new DateTime(2023, 6, 20, 12, 0, 0);
        const double aggregatedPrice = 15;
        _priceRepositoryMock
            .Setup(repo => repo.GetPrice(Instrument, time))
            .ReturnsAsync(0);
        _priceAggregatorMock
            .Setup(aggregator => aggregator.AggregatePrice(It.IsAny<IReadOnlyList<IPriceSource>>(), Instrument, time))
            .ReturnsAsync(aggregatedPrice);

        // Act
        AggregatedPriceModel result = await _priceService.GetAggregatedPrice(Instrument, time);

        // Assert
        result.AggregatedPrice.Should().Be(aggregatedPrice);
        _priceRepositoryMock.Verify(repo => repo.GetPrice(Instrument, time), Times.Once);
        _priceAggregatorMock.Verify(aggregator => aggregator.AggregatePrice(It.IsAny<IReadOnlyList<IPriceSource>>(), Instrument, time), Times.Once);
        _priceRepositoryMock.Verify(repo => repo
            .SavePrice(It.Is<PriceAggregate>(price => price.Instrument == Instrument.ToLower() && price.Time == time && price.Price == aggregatedPrice)), Times.Once);
    }
    
    [Fact]
    public async Task GivenTheRepositoryReturnAListOfPrices_WhenGettingThePersistedPrices_ThenItShouldReturnTheListOfTheAggregatedPrices()
    {
        // Arrange
        var start = new DateTime(2023, 6, 1, 0, 0, 0);
        var end = new DateTime(2023, 6, 30, 23, 59, 59);
        var formattedEnd = new DateTime(2023, 6, 30, 23, 0, 0);
        var prices = new List<PriceAggregate>
        {
            new(Instrument, new DateTime(2023, 6, 1, 0, 0, 0),  10),
            new(Instrument, new DateTime(2023, 6, 1, 0, 0, 0),  15) 
        };

        var expectedModels = new List<AggregatedPriceModel>
        {
            new() { AggregatedPrice = 10, Time = new DateTime(2023, 6, 1, 0, 0, 0) },
            new() { AggregatedPrice = 15, Time = new DateTime(2023, 6, 1, 0, 0, 0) }
        };
        _priceRepositoryMock
            .Setup(repo => repo.GetPrices(Instrument, start, formattedEnd))
            .ReturnsAsync(prices);

        // Act
        var result = await _priceService.GetPersistedPrices(Instrument, start, end);

        // Assert
        result.Should().BeEquivalentTo(expectedModels);
        _priceRepositoryMock.Verify(repo => repo.GetPrices(Instrument, start, formattedEnd), Times.Once);
    }
    
    [Fact]
    public async Task GivenTheRepositoryReturnAnEmptyListOfPrices_WhenGettingThePersistedPrices_ThenItShouldReturnAnEmptyListOfTheAggregatedPrices()
    {
        // Arrange
        var start = new DateTime(2023, 6, 1, 0, 0, 0);
        var end = new DateTime(2023, 6, 30, 23, 59, 59);
        var formattedEnd = new DateTime(2023, 6, 30, 23, 0, 0);
        _priceRepositoryMock
            .Setup(repo => repo.GetPrices(Instrument, start, formattedEnd))
            .ReturnsAsync(new List<PriceAggregate>());

        // Act
        var result = await _priceService.GetPersistedPrices(Instrument, start, end);

        // Assert
        result.Should().BeEmpty();
        _priceRepositoryMock.Verify(repo => repo.GetPrices(Instrument, start, formattedEnd), Times.Once);
    }
}