using System.Text.Json;
using EnsureThat;
using Microsoft.Extensions.Options;
using PriceAggregator.API.Configurations;

namespace PriceAggregator.API.Services.External.Bitstamp;

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

    public async Task<PriceSourceResponse> GetPrice(string financialInstrument, DateTime time)
    {
        try
        {
            var response = await _httpClient.GetAsync(_bitstampConfiguration.InstrumentPriceEndpointUri(financialInstrument, Step, Limit, time));
            response.EnsureSuccessStatusCode();

            //var testcontent = await response.Content.ReadAsStringAsync();
                
            var apiResponse = JsonSerializer.Deserialize<BitstampResponse>(await response.Content.ReadAsStringAsync());

            return apiResponse is { Data: { Candles.Count: > 0 } }
                ? PriceSourceResponse.Success(apiResponse.Data.Candles.FirstOrDefault().Close) 
                : PriceSourceResponse.Failure();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while fetching price from Bitstamp API");
            
            return PriceSourceResponse.Failure();
        }
    }
}