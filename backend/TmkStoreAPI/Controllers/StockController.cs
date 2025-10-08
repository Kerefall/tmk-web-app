using Microsoft.AspNetCore.Mvc;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockController : ControllerBase
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpGet]
        public async Task<ActionResult<List<StockResponse>>> GetStocks()
        {
            var stocks = await _stockService.GetAllStocks();

            var response = stocks.Select(s => new StockResponse(
                s.IDStock,
                s.City,
                s.StockName));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<StockResponse>> GetStockById(Guid id)
        {
            var stock = await _stockService.GetStockById(id);

            if (stock == null)
            {
                return NotFound();
            }

            var response = new StockResponse(
                stock.IDStock,
                stock.City,
                stock.StockName);

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<StockResponse>>> SearchStocks(
            [FromQuery] string? city = null,
            [FromQuery] string? stockName = null)
        {
            var stocks = await _stockService.SearchStocks(city, stockName);

            var response = stocks.Select(s => new StockResponse(
                s.IDStock,
                s.City,
                s.StockName));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateStock([FromBody] StockRequest request)
        {
            var (stock, error) = Stock.Create(
                Guid.NewGuid(),
                request.City,
                request.StockName);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var stockId = await _stockService.CreateStock(stock);

            return Ok(stockId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateStock(Guid id, [FromBody] StockRequest request)
        {
            var (stock, error) = Stock.Create(
                id,
                request.City,
                request.StockName);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var stockId = await _stockService.UpdateStock(stock);

            return Ok(stockId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteStock(Guid id)
        {
            var stockId = await _stockService.DeleteStock(id);

            return Ok(stockId);
        }
    }
}