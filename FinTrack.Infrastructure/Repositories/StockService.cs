// Service layer here.
// My goal is to separate business logic from the repository.
// I handle all stock-related operations here, repository only deals with db.

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
            // getting all stocks and mapping to dto
            var stocks = await _stockRepository.GetAllAsync();

            return stocks.Select(s => new StockDto
            {
                Id = s.Id,
                Symbol = s.Symbol,
                Name = s.Name
            }).ToList();
        }

        public async Task AddStockAsync(CreateStockDto dto)
        {
            // converting dto to entity
            // making symbol uppercase
            var stock = new Stock
            {
                Symbol = dto.Symbol.ToUpper(),
                Name = dto.Name
            };

            await _stockRepository.AddAsync(stock);
        }

        public async Task DeleteStockAsync(int id)
        {
            // deleting stock by id
            await _stockRepository.DeleteAsync(id);
        }

        public async Task<decimal> RefreshStockPriceAsync(string symbol)
        {
            // getting real price from finnhub api
            var apiKey = _config["Finnhub:ApiKey"];

            var url = $"https://finnhub.io/api/v1/quote?symbol={symbol}&token={apiKey}";

            var response = await _httpClient.GetStringAsync(url);

            var json = System.Text.Json.JsonDocument.Parse(response);

            var price = json.RootElement.GetProperty("c").GetDecimal();

            // saving fetched price to database
            await _stockRepository.SavePriceAsync(symbol.ToUpper(), price);

            return price;
        }

        public async Task<List<object>> GetTopGainersAsync()
        {
            // using fake data for now, can be improved later
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
            // using fake data for now, can be improved later
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
    }
}