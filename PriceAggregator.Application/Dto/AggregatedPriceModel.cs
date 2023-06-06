namespace PriceAggregator.Application.Dto;

public class AggregatedPriceModel
{
    public DateTime Time { get; set; }
    
    public double AggregatedPrice { get; set; }
}