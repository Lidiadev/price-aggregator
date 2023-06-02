using PriceAggregator.API.Services;
﻿using Microsoft.EntityFrameworkCore;
using PriceAggregator.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories here
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IPriceAggregatorService, PriceAggregatorService>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();

// Configure the in-memory database
builder.Services.AddDbContext<FinancialInstrumentsDbContext>(options =>
    options.UseInMemoryDatabase(databaseName: "FinancialInstrumentInMemoryDatabase"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }

