namespace PriceAggregator.API.Models;

public class AggregatedPriceModel
{
    public DateTime Time { get; set; }
    
    public double AggregatedPrice { get; set; }
}