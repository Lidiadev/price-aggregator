using PriceAggregator.API.Models;

namespace PriceAggregator.API.Repository;

public interface IPriceRepository
{
    Task SavePrice(PriceModel newPrice);
}