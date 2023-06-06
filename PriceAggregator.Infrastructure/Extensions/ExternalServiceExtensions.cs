using Microsoft.Extensions.DependencyInjection;
using PriceAggregator.Infrastructure.Configurations.Configurations;
using PriceAggregator.Infrastructure.ExternalDependencies;
using PriceAggregator.Infrastructure.ExternalDependencies.Bitfinex;
using PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

namespace PriceAggregator.Infrastructure.Extensions;

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