using Microsoft.AspNetCore.Mvc;
using PriceAggregator.API.Models;
using PriceAggregator.API.Services;

namespace PriceAggregator.API.Controllers;

[ApiController]
[Route("[controller]")]
public class PricesController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly ILogger<PricesController> _logger;

    public PricesController(IPriceService priceService, ILogger<PricesController> logger)
    {
        _priceService = priceService;
        _logger = logger;
    }

    [HttpGet("{time:datetime}")]
    public async Task<ActionResult<double>> GetAggregatedPrice(DateTime time)
    {
        return await _priceService.GetAggregatedPrice(time);
    }
    
    [HttpGet("range")]
    public async Task<ActionResult<List<AggregatedPriceModel>>> GetPersistedPrices(DateTime start, DateTime end)
    {
        return await _priceService.GetPersistedPrices(start, end);
    }
}

