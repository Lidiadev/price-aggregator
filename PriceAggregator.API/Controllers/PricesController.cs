using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PriceAggregator.API.Models;
using PriceAggregator.API.Services;

namespace PriceAggregator.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class PricesController : ControllerBase
{
    private readonly IPriceService _priceService;
    private readonly ILogger<PricesController> _logger;

    public PricesController(IPriceService priceService, ILogger<PricesController> logger)
    {
        _priceService = priceService;
        _logger = logger;
    }

    [HttpGet("{instrument}/{time:datetime}")]
    public async Task<ActionResult<double>> GetAggregatedPrice(string instrument, DateTime time)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var aggregatedPrice = await _priceService.GetAggregatedPrice(instrument, time);

            return aggregatedPrice.AggregatedPrice;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
    
    [HttpGet("persisted/{instrument}")]
    public async Task<ActionResult<List<AggregatedPriceModel>>> GetPersistedPrices(
        string instrument, 
        [Required] DateTime startTime, 
        [Required] DateTime endTime)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return await _priceService.GetPersistedPrices(instrument, startTime, endTime);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}

