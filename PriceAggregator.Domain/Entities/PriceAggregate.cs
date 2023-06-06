using PriceAggregator.Domain.ValueObjects;

namespace PriceAggregator.Domain.Entities;

public class PriceAggregate
{
    public int Id { get; private set; }
    
    public FinancialInstrument Instrument { get; private set; }
    
    public DateTime Time { get; private set; }
    
    public double Price { get; private set; }
    
    private PriceAggregate() { }

    public PriceAggregate(string instrument, DateTime time, double price)
    {
        Instrument = new FinancialInstrument(instrument);
        Time = time;
        Price = price;
    }
}

