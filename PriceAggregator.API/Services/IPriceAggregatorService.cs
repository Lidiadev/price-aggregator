namespace PriceAggregator.API.Services;

public interface IPriceAggregatorService
{
    Task<double> AggregatePrice(string financialInstrument, DateTime time);
}