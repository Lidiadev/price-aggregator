using PriceAggregator.API.Services;
using Microsoft.EntityFrameworkCore;
using PriceAggregator.API.Configurations;
using PriceAggregator.API.Extensions;
using PriceAggregator.API.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register configuration
 builder.Services.Configure<BitstampConfiguration>(builder.Configuration.GetSection(nameof(BitstampConfiguration)));
 builder.Services.Configure<BitfinexConfiguration>(builder.Configuration.GetSection(nameof(BitfinexConfiguration)));

// Register http clients
builder.Services.AddBitstampHttpClient(builder.Configuration.GetSection(nameof(BitstampConfiguration)).Get<BitstampConfiguration>());
builder.Services.AddBitfinexHttpClient(builder.Configuration.GetSection(nameof(BitfinexConfiguration)).Get<BitfinexConfiguration>());

// Register services and repositories 
builder.Services.AddScoped<IPriceService, PriceService>();
builder.Services.AddScoped<IPriceAggregator, PriceAggregator.API.Services.PriceAggregator>();
builder.Services.AddScoped<IPriceRepository, PriceRepository>();
builder.Services.AddScoped<IPriceAggregationStrategy, PriceAverageAggregationStrategy>();

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

