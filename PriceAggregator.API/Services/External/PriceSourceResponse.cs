namespace PriceAggregator.API.Services.External;

public class PriceSourceResponse
{
    public double Price { get; }
    
    public bool IsSuccessful { get; }

    private PriceSourceResponse(double price)
    {
        Price = price;
        IsSuccessful = true;
    }
    
    private PriceSourceResponse(bool isSuccessful)
    {
        IsSuccessful = isSuccessful;
    }

    public static PriceSourceResponse Success(double price) =>
        new(price);

    public static PriceSourceResponse Failure() =>
        new(false);
}