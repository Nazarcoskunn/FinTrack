using FinTrack.Application.DTOs;
using FinTrack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StocksController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StocksController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var stocks = await _stockService.GetAllStocksAsync();
            return Ok(stocks);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateStockDto dto)
        {
            await _stockService.AddStockAsync(dto);
            return Ok("Stock added successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _stockService.DeleteStockAsync(id);
            return Ok("Stock deleted successfully.");
        }

        [HttpPost("{symbol}/refresh-price")]
        public async Task<IActionResult> RefreshPrice(string symbol)
        {
            var price = await _stockService.RefreshStockPriceAsync(symbol);
            return Ok(new { Symbol = symbol.ToUpper(), Price = price });
        }

        [HttpGet("top-gainers")]
        public async Task<IActionResult> GetTopGainers()
        {
            var result = await _stockService.GetTopGainersAsync();
            return Ok(result);
        }

        [HttpGet("top-losers")]
        public async Task<IActionResult> GetTopLosers()
        {
            var result = await _stockService.GetTopLosersAsync();
            return Ok(result);
        }
    }
}