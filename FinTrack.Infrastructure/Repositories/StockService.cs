// Service layer is used here.
// The goal is to separate business logic from the repository.
// All stock-related operations (add, delete, price update, analytics)
// are handled here, while the repository only deals with database operations.

using FinTrack.Application.DTOs;
using FinTrack.Application.Interfaces;
using FinTrack.Domain.Entities;
using Microsoft.Extensions.Configuration;

namespace FinTrack.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _config;

        public StockService(IStockRepository stockRepository, HttpClient httpClient, IConfiguration config)
        {
            _stockRepository = stockRepository;
            _httpClient = httpClient;
            _config = config;
        }

public async Task<List<StockDto>> GetAllStocksAsync()
{
    var stocks = await _stockRepository.GetAllAsync();

    return stocks.Select(s =>
    {
        var lastPrice = s.Prices
            .OrderByDescending(p => p.PriceDate)
            .FirstOrDefault();

        return new StockDto
        {
            Id = s.Id,
            Symbol = s.Symbol,
            Name = s.Name,
            LastPrice = lastPrice?.Price,
            LastUpdated = lastPrice?.PriceDate
        };
    }).ToList();
}
        public async Task AddStockAsync(CreateStockDto dto)
        {
            var stock = new Stock
            {
                Symbol = dto.Symbol.ToUpper(),
                Name = dto.Name
            };

            await _stockRepository.AddAsync(stock);
        }

        public async Task DeleteStockAsync(int id)
        {
            await _stockRepository.DeleteAsync(id);
        }
        public async Task<decimal> RefreshStockPriceAsync(string symbol)
        {
            // getting API key from environment variable for security
            var apiKey = Environment.GetEnvironmentVariable("FINNHUB_API_KEY");
            // fetching real-time stock price from Finnhub API
            var url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={apiKey}";

            var response = await _httpClient.GetStringAsync(url);

            var json = System.Text.Json.JsonDocument.Parse(response);

            var price = json.RootElement.GetProperty("c").GetDecimal();

            await _stockRepository.SavePriceAsync(symbol.ToUpper(), price);

            return price;
        }

        public async Task<List<object>> GetTopGainersAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();

            return stocks
                .Select(s => new
                {
                    s.Symbol,
                    Growth = new Random().Next(1, 20)
                })
                .OrderByDescending(x => x.Growth)
                .Take(5)
                .Cast<object>()
                .ToList();
        }

        public async Task<List<object>> GetTopLosersAsync()
        {
            var stocks = await _stockRepository.GetAllAsync();

            return stocks
                .Select(s => new
                {
                    s.Symbol,
                    Loss = new Random().Next(1, 20)
                })
                .OrderByDescending(x => x.Loss)
                .Take(5)
                .Cast<object>()
                .ToList();
        }


        // This method calculates the average price for each stock.
        // Goal: to show that I can process data, not just fetch it.
        // I take price history from StockPrice table and calculate the average.

        public async Task<List<object>> GetAveragePricesAsync()
        {

            var stocks = await _stockRepository.GetAllAsync();

            var result = stocks
                .Select(s =>
                {

                    if (s.Prices == null || s.Prices.Count == 0)
                        return null;

                    // calculate average price
                    var avg = s.Prices.Average(p => p.Price);


                    return new
                    {
                        Symbol = s.Symbol,
                        AveragePrice = Math.Round(avg, 2)
                    };
                })

                .Where(x => x != null)

                .Cast<object>()
                .ToList();

            return result;
        }
    }
}