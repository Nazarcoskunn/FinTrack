namespace FinTrack.Application.DTOs
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = null!;
        public string Name { get; set; } = null!;
        public decimal? LastPrice { get; set; }
        public DateTime? LastUpdated { get; set; }
    }
}