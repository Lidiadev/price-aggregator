namespace PriceAggregator.API.Models;

public class PriceData
{
    public int Id { get; }
    
    public string Instrument { get; set; }
    
    public DateTime Time { get; set; }
    
    public double Price { get; set; }

    public PriceData(string instrument, DateTime time, double price)
    {
        Instrument = instrument.ToLower();
        Time = time;
        Price = price;
    }
}

