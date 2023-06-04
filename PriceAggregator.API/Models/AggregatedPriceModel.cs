namespace PriceAggregator.API.Models;

public class AggregatedPriceModel
{
    public DateTime Time { get; set; }
    
    public decimal AggregatedPrice { get; set; }
}