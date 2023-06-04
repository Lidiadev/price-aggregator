namespace PriceAggregator.API.Services.External;

public class PriceSourceResponse
{
    public decimal Price { get; }
    
    public bool IsSuccessful { get; }

    private PriceSourceResponse(decimal price)
    {
        Price = price;
        IsSuccessful = true;
    }
    
    private PriceSourceResponse(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public static PriceSourceResponse Success(decimal price) =>
        new(price);

    public static PriceSourceResponse Failure() =>
        new(false);
}