using System.Text.Json;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PriceAggregator.Infrastructure.Configurations.Configurations;
using PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

namespace PriceAggregator.Infrastructure.ExternalDependencies.Bitfinex;

public class BitfinexService : IPriceSource
{
    private const string Step = "1h";
    private const int Limit = 1;
    
    private readonly HttpClient _httpClient;
    private readonly BitfinexConfiguration _bitfinexConfiguration;
    private readonly ILogger<BitstampService> _logger;

    public BitfinexService(
        HttpClient httpClient, 
        IOptions<BitfinexConfiguration> bitfinexConfiguration,
        ILogger<BitstampService> logger)
    {
        _httpClient = httpClient;
        _bitfinexConfiguration = EnsureArg.IsNotNull(bitfinexConfiguration.Value, nameof(BitfinexConfiguration));
        _logger = logger;
    }

    public async Task<PriceSourceResponse> GetPrice(string instrument, DateTime time)
    {
        try
        {
            var response = await _httpClient.GetAsync(
                    _bitfinexConfiguration.InstrumentPriceEndpointUri(instrument, Step, Limit, time));
            response.EnsureSuccessStatusCode();

            var apiResponse = JsonSerializer.Deserialize<List<List<object>>>(await response.Content.ReadAsStringAsync());

            return apiResponse is { Count: > 0 }
                ? PriceSourceResponse.Success(double.Parse(apiResponse.FirstOrDefault()[1].ToString()))
                : PriceSourceResponse.Failure();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching price from Bitfinex API");

            return PriceSourceResponse.Failure();
        }
    }
}