namespace PriceAggregator.API.Configurations;

public class ExternalServiceConfiguration
{
    public string ServiceBaseUrl { get; set; }

    public string InstrumentPriceEndpoint { get; set; }
    
    public bool IsEnabled { get; set; }
    
    public Uri BaseUri => new(ServiceBaseUrl);
}