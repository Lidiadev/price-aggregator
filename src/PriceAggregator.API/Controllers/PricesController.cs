using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using PriceAggregator.Application.Dto;
using PriceAggregator.Application.Services;

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
    [ProducesResponseType(typeof(double), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetAggregatedPrice([DefaultValue("btcusd")]string instrument, [DefaultValue("2023-06-02T10:00:00")]DateTime time)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            var aggregatedPrice = await _priceService.GetAggregatedPrice(instrument, time);

            return Ok(aggregatedPrice.AggregatedPrice);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
    
    [HttpGet("persisted/{instrument}")]
    [ProducesResponseType(typeof(List<AggregatedPriceModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetPersistedPrices(
        [DefaultValue("btcusd")] string instrument, 
        [Required] [DefaultValue("2023-06-02T10:00:00")] DateTime startTime, 
        [Required] [DefaultValue("2023-06-03T10:00:00")] DateTime endTime)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            
            return Ok(await _priceService.GetPersistedPrices(instrument, startTime, endTime));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, ex.Message);
            return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}

