// Service Pattern interface.

using FinTrack.Application.DTOs;
using FinTrack.Domain.Entities;

namespace FinTrack.Application.Interfaces
{
    public interface IStockService
    {
        Task<List<StockDto>> GetAllStocksAsync();
        Task AddStockAsync(CreateStockDto dto);
        Task DeleteStockAsync(int id);

        Task<decimal> RefreshStockPriceAsync(string symbol);

        Task<List<object>> GetTopGainersAsync();
        Task<List<object>> GetTopLosersAsync();
        Task<List<object>> GetAveragePricesAsync();
    }
}