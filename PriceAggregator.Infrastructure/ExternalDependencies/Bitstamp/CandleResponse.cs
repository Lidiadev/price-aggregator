using System.Text.Json.Serialization;

namespace PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

public class CandleResponse
{
    [JsonPropertyName("ohlc")]
    public List<CandleData> Candles { get; set; }

    [JsonPropertyName("pair")]
    public string Pair { get; set; }
}