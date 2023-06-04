using System.Runtime.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class CandleResponse
{
    [DataMember(Name = "close")]
    public decimal Close { get; set; }

    [DataMember(Name = "high")]
    public decimal High { get; set; }

    [DataMember(Name = "low")]
    public decimal Low { get; set; }

    [DataMember(Name = "open")]
    public decimal Open { get; set; }

    [DataMember(Name = "timestamp")]
    public string Timestamp { get; set; }

    [DataMember(Name = "volume")]
    public string Volume { get; set; }
}