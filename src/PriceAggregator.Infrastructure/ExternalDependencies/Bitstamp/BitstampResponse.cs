using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PriceAggregator.Infrastructure.ExternalDependencies.Bitstamp;

[DataContract]
public class BitstampResponse
{
    [JsonPropertyName("data")]
    public CandleResponse Data { get; set; }
}