using PriceAggregator.API.Configurations;
using PriceAggregator.API.Services;
using PriceAggregator.API.Services.External.Bitstamp;

namespace PriceAggregator.API.Extensions;

public static class BitstampServiceExtensions
{
    public static IServiceCollection AddBitstampHttpClient(
        this IServiceCollection services,
        BitstampConfiguration? bitstampConfiguration)
    {
        if (bitstampConfiguration?.IsEnabled == true)
        {
            services
                .AddHttpClient<IPriceSource, BitstampService>(client =>
                    client.BaseAddress = bitstampConfiguration.BaseUri);
        }

        return services;
    }
}