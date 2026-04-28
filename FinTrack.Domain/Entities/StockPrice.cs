using System;

namespace FinTrack.Domain.Entities
{
    public class StockPrice
    {
        public int Id { get; set; }

        public int StockId { get; set; }

        public decimal Price { get; set; }

        public DateTime PriceDate { get; set; } = DateTime.UtcNow;

        public Stock Stock { get; set; } = null!;
    }
}