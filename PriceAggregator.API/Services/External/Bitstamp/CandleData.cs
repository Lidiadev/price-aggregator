using System.Runtime.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class CandleData
{
    [DataMember(Name = "close")]
    public double Close { get; set; }

    [DataMember(Name = "high")]
    public double High { get; set; }

    [DataMember(Name = "low")]
    public double Low { get; set; }

    [DataMember(Name = "open")]
    public double Open { get; set; }

    [DataMember(Name = "timestamp")]
    public string Timestamp { get; set; }
}