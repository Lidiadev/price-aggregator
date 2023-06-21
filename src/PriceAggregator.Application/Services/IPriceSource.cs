using PriceAggregator.Application.Dto;

namespace PriceAggregator.Application.Services;

public interface IPriceSource
{
    Task<PriceSourceResponse> GetPrice(string instrument, DateTime time);
}