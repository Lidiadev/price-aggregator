using System.Runtime.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class CandleResponse
{
    [DataMember(Name = "ohlc")]
    public List<CandleData> Candles { get; set; }

    [DataMember(Name = "pair")]
    public string Pair { get; set; }
}