using PriceAggregator.API.Configurations;
using PriceAggregator.API.Services;
using PriceAggregator.API.Services.External.Bitfinex;
using PriceAggregator.API.Services.External.Bitstamp;

namespace PriceAggregator.API.Extensions;

public static class ExternalServiceExtensions
{
    public static IServiceCollection AddBitstampHttpClient(
        this IServiceCollection services,
        BitstampConfiguration? bitstampConfiguration)
    {
        if (bitstampConfiguration?.IsEnabled == true)
        {
            services
                .AddHttpClient<IPriceSource, BitstampService>(bitstampConfiguration.HttpClientName, client =>
                    client.BaseAddress = bitstampConfiguration.BaseUri);
        }

        return services;
    }

    public static IServiceCollection AddBitfinexHttpClient(
        this IServiceCollection services,
        BitfinexConfiguration? bitfinexConfiguration)
    {
        if (bitfinexConfiguration?.IsEnabled == true)
        {
            services
                .AddHttpClient<IPriceSource, BitfinexService>(bitfinexConfiguration.HttpClientName, client =>
                    client.BaseAddress = bitfinexConfiguration.BaseUri);
        }

        return services;
    }
}