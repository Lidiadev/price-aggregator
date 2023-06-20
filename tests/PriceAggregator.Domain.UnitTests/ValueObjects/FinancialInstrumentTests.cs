using FluentAssertions;
using PriceAggregator.Domain.ValueObjects;

namespace PriceAggregator.Domain.UnitTests.ValueObjects;

public class FinancialInstrumentTests
{
    private const string Symbol = "BTCUSD";
    [Fact]
    public void GivenTheSymbolIsValid_WhenCreatingAFinancialSymbol_ThenItShouldCreateANewInstance()
    {
        // Arrange
        // Act
        var instrument = FinancialInstrument.From(Symbol);

        // Assert
        instrument.Symbol.Should().Be(Symbol.ToLower());
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    [InlineData("  ")]
    public void GivenTheSymbolIsNotValid_WhenCreatingAFinancialInstrument_ThenItShouldThrowArgumentException(string symbol)
    {
        // Act & Assert
        FluentActions.Invoking(() => FinancialInstrument.From(symbol))
            .Should().Throw<ArgumentException>()
            .WithMessage("The financial instrument symbol must be provided. (Parameter 'symbol')")
            .And.ParamName.Should().Be("symbol");
    }

    [Fact]
    public void GivenTheSymbolIsValid_WhenGettingTheHashCode_ThenItShouldReturnTheHashCodeOfSymbol()
    {
        // Arrange
        var instrument = FinancialInstrument.From(Symbol);
        var expectedHashCode = Symbol.ToLower().GetHashCode();

        // Act
        var hashCode = instrument.GetHashCode();

        // Assert
        hashCode.Should().Be(expectedHashCode);
    }

    [Fact]
    public void GivenFinancialInstruments_WhenComparingTheFinancialInstruments_ThenItShouldReturnExpectedResults()
    {
        // Arrange
        var instrument1 = FinancialInstrument.From(Symbol);
        var instrument2 = FinancialInstrument.From(Symbol);

        // Act & Assert
        instrument1.Equals(instrument2).Should().BeTrue();
    }
    
    [Fact]
    public void GivenSymbol_WhenComparingToFinancialInstrument_ThenItShouldReturnExpectedResults()
    {
        // Arrange
        var instrument = FinancialInstrument.From(Symbol);

        // Act & Assert
        (instrument == Symbol.ToLower()).Should().BeTrue();
        (instrument != Symbol.ToLower()).Should().BeFalse();
    }
}