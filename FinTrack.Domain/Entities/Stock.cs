
namespace FinTrack.Domain.Entities
{
    public class Stock
    {
        public int Id { get; set; }

        public string Symbol { get; set; } = null!; 

        public string Name { get; set; } = null!; 

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public List<StockPrice> Prices { get; set; } = new();
       
    }
}