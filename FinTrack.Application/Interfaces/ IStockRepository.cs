// Repository Pattern interface.

using FinTrack.Domain.Entities;

namespace FinTrack.Application.Interfaces
{
    public interface IStockRepository
    {
        Task<List<Stock>> GetAllAsync();
        Task AddAsync(Stock stock);
        Task DeleteAsync(int id);
        Task SavePriceAsync(string symbol, decimal price);
    }
}