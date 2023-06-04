﻿namespace PriceAggregator.API.Models;

public class PriceData
{
    public int Id { get; set; }
    
    public DateTime Time { get; set; }
    
    public decimal Price { get; set; }
}

