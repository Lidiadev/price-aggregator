namespace PriceAggregator.API.Services.External.Bitfinex;

public class CandleData
{
    public long Timestamp { get; set; }
    
    public double Open { get; set; }
    
    public double High { get; set; }
    
    public double Low { get; set; }
    public double Close { get; set; }
}