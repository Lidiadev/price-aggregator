namespace PriceAggregator.Infrastructure.Configurations.Configurations;

public abstract class ExternalServiceConfiguration
{
    public abstract string ClientName { get; } 
    
    public string ServiceBaseUrl { get; set; }

    public string InstrumentPriceEndpoint { get; set; }
    
    public bool IsEnabled { get; set; }
    
    public Uri BaseUri => new(ServiceBaseUrl);
}