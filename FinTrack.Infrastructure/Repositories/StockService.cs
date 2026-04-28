// Service layer burada.
// Amaç: business logic'i repository'den ayırmak.
// Stock ile ilgili işlemleri (ekleme, silme, fiyat güncelleme, analiz)
// burada yapıyorum, repository sadece database ile ilgileniyor.

using FinTrack.Application.DTOs;
using FinTrack.Application.Interfaces;
using FinTrack.Domain.Entities;

namespace FinTrack.Infrastructure.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<StockDto>> GetAllStocksAsync()
        {
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
            // Temporary fake price. Finnhub API will replace this later.
            var price = new Random().Next(100, 500);

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
    }
}