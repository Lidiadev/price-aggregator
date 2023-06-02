using Microsoft.AspNetCore.Mvc;

namespace PriceAggregator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController : ControllerBase
{
    private readonly ILogger<PricesController> _logger;

    public PricesController(ILogger<PricesController> logger)
    {
        _logger = logger;
    }

    public IEnumerable<PriceModel> Get()
    {
        return new List<PriceModel>
        {
            new()
            {
                Price = 10,
                Time = DateTime.Now
            }
        };
    }
}

