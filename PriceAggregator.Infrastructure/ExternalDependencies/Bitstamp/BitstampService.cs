using System.Text.Json;
using EnsureThat;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PriceAggregator.Application.Dto;
using PriceAggregator.Application.Services;
using PriceAggregator.Infrastructure.Configurations;

namespace PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

public class BitstampService : IPriceSource
{
    private const int Step = 3600;
    private const int Limit = 1;

    private readonly HttpClient _httpClient;
    private readonly ILogger<BitstampService> _logger;
    private readonly BitstampConfiguration _bitstampConfiguration;

    public BitstampService(
        HttpClient httpClient, 
        IOptions<BitstampConfiguration> bitstampConfiguration,
        ILogger<BitstampService> logger)
    {
        _httpClient = httpClient;
        _logger = logger;
        _bitstampConfiguration = EnsureArg.IsNotNull(bitstampConfiguration.Value, nameof(BitstampConfiguration));
    }

    public async Task<PriceSourceResponse> GetPrice(string instrument, DateTime time)
    {
        try
        {
            var response = await _httpClient.GetAsync(_bitstampConfiguration.InstrumentPriceEndpointUri(instrument, Step, Limit, time));
            response.EnsureSuccessStatusCode();

            var apiResponse = JsonSerializer.Deserialize<BitstampResponse>(await response.Content.ReadAsStringAsync(),
                new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

            return apiResponse is { Data: { Candles.Count: > 0 } }
                ? PriceSourceResponse.Success(double.Parse(apiResponse.Data.Candles.FirstOrDefault().Close)) 
                : PriceSourceResponse.Failure();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching price from Bitstamp API");
            
            return PriceSourceResponse.Failure();
        }
    }
}