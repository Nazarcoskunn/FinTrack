// Repository Pattern is used here to separate data access logic from business logic.
// This makes the code more maintainable and easier to test.


using FinTrack.Application.Interfaces;
using FinTrack.Domain.Entities;
using FinTrack.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace FinTrack.Infrastructure.Repositories
{
    
    public class StockRepository : IStockRepository
    {
        private readonly AppDbContext _context;

        public StockRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetAllAsync()
        {
            return await _context.Stocks.Include(s => s.Prices).ToListAsync();
        }

        public async Task AddAsync(Stock stock)
        {
            _context.Stocks.Add(stock);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var stock = await _context.Stocks.FindAsync(id);
            if (stock != null)
            {
                _context.Stocks.Remove(stock);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SavePriceAsync(string symbol, decimal price)
        {
            var stock = await _context.Stocks.FirstOrDefaultAsync(s => s.Symbol == symbol);

            if (stock == null) return;

            var stockPrice = new StockPrice
            {
                StockId = stock.Id,
                Price = price,
                PriceDate = DateTime.UtcNow
            };

            _context.StockPrices.Add(stockPrice);
            await _context.SaveChangesAsync();
        }
    }
}