using System.Runtime.Serialization;

namespace PriceAggregator.API.Services.External.Bitstamp;

[DataContract]
public class BitstampResponse
{
    [DataMember(Name = "data")]
    public CandleResponse Data { get; set; }
}