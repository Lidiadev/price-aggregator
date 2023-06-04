using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class BitstampResponse
{
    [JsonPropertyName("data")]
    public CandleResponse Data { get; set; }
}