using System.Runtime.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class BitstampData
{
    [DataMember(Name = "ohlc")]
    public List<CandleResponse> Ohlc { get; set; }

    [DataMember(Name = "pair")]
    public string Pair { get; set; }
}