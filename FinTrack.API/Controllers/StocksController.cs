using FinTrack.Application.DTOs;
using FinTrack.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace FinTrack.API.Controllers
{
    // Controller layer handles HTTP requests and responses.
    // I receive requests here and pass them to the service layer.

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
            try
            {
                await _stockService.AddStockAsync(dto);
                return Ok("Stock added successfully.");
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Error while adding stock."
                });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _stockService.DeleteStockAsync(id);
                return Ok("Stock deleted successfully.");
            }
            catch (Exception)
            {
                return BadRequest(new
                {
                    message = "Error while deleting stock."
                });
            }
        }
        [HttpPost("{symbol}/refresh-price")]
        public async Task<IActionResult> RefreshPrice(string symbol)
        {
            try
            {
                var price = await _stockService.RefreshStockPriceAsync(symbol);

                return Ok(new
                {
                    Symbol = symbol.ToUpper(),
                    Price = price
                });
            }
            catch (Exception)
            {
                return StatusCode(500, new
                {
                    message = "Error occurred while fetching stock price."
                });
            }
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
        [HttpGet("average-prices")]
        public async Task<IActionResult> GetAveragePrices()
        {
            var result = await _stockService.GetAveragePricesAsync();
            return Ok(result);
        }
    }
}