using Microsoft.Extensions.DependencyInjection;
using PriceAggregator.Application.Services;
using PriceAggregator.Infrastructure.Configurations;
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
                .AddHttpClient<IPriceSource, BitstampService>(bitstampConfiguration.ClientName, client =>
                {
                    client.BaseAddress = bitstampConfiguration.BaseUri;
                    client.Timeout = TimeSpan.FromMilliseconds(bitstampConfiguration.Timeout);
                });
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
                .AddHttpClient<IPriceSource, BitfinexService>(bitfinexConfiguration.ClientName, client =>
                {
                    client.BaseAddress = bitfinexConfiguration.BaseUri;
                    client.Timeout = TimeSpan.FromMilliseconds(bitfinexConfiguration.Timeout);
                });
        }

        return services;
    }
}