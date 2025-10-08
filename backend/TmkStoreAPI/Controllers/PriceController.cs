using Microsoft.AspNetCore.Mvc;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PriceController : ControllerBase
    {
        private readonly IPriceService _priceService;

        public PriceController(IPriceService priceService)
        {
            _priceService = priceService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PriceResponse>>> GetPrices()
        {
            var prices = await _priceService.GetAllPrices();

            var response = prices.Select(p => new PriceResponse(
                p.ID,
                p.IDStock,
                p.PriceT,
                p.PriceLimitT1,
                p.PriceT1,
                p.PriceLimitT2,
                p.PriceT2,
                p.PriceM,
                p.PriceLimitM1,
                p.PriceM1,
                p.PriceLimitM2,
                p.PriceM2,
                p.NDS));

            return Ok(response);
        }

        [HttpGet("nomenclature/{nomenclatureId:guid}")]
        public async Task<ActionResult<List<PriceResponse>>> GetPricesByNomenclature(Guid nomenclatureId)
        {
            var prices = await _priceService.GetPricesByNomenclatureId(nomenclatureId);

            var response = prices.Select(p => new PriceResponse(
                p.ID,
                p.IDStock,
                p.PriceT,
                p.PriceLimitT1,
                p.PriceT1,
                p.PriceLimitT2,
                p.PriceT2,
                p.PriceM,
                p.PriceLimitM1,
                p.PriceM1,
                p.PriceLimitM2,
                p.PriceM2,
                p.NDS));

            return Ok(response);
        }

        [HttpGet("stock/{stockId:guid}")]
        public async Task<ActionResult<List<PriceResponse>>> GetPricesByStock(Guid stockId)
        {
            var prices = await _priceService.GetPricesByStockId(stockId);

            var response = prices.Select(p => new PriceResponse(
                p.ID,
                p.IDStock,
                p.PriceT,
                p.PriceLimitT1,
                p.PriceT1,
                p.PriceLimitT2,
                p.PriceT2,
                p.PriceM,
                p.PriceLimitM1,
                p.PriceM1,
                p.PriceLimitM2,
                p.PriceM2,
                p.NDS));

            return Ok(response);
        }

        [HttpGet("nomenclature/{nomenclatureId:guid}/stock/{stockId:guid}")]
        public async Task<ActionResult<PriceResponse>> GetPriceByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            var price = await _priceService.GetPriceByNomenclatureAndStock(nomenclatureId, stockId);

            if (price == null)
            {
                return NotFound();
            }

            var response = new PriceResponse(
                price.ID,
                price.IDStock,
                price.PriceT,
                price.PriceLimitT1,
                price.PriceT1,
                price.PriceLimitT2,
                price.PriceT2,
                price.PriceM,
                price.PriceLimitM1,
                price.PriceM1,
                price.PriceLimitM2,
                price.PriceM2,
                price.NDS);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePrice([FromBody] PriceRequest request)
        {
            var (price, error) = Price.Create(
                request.ID,
                request.IDStock,
                request.PriceT,
                request.PriceLimitT1,
                request.PriceT1,
                request.PriceLimitT2,
                request.PriceT2,
                request.PriceM,
                request.PriceLimitM1,
                request.PriceM1,
                request.PriceLimitM2,
                request.PriceM2,
                request.NDS);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var priceId = await _priceService.CreatePrice(price);

            return Ok(priceId);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdatePrice([FromBody] PriceRequest request)
        {
            var (price, error) = Price.Create(
                request.ID,
                request.IDStock,
                request.PriceT,
                request.PriceLimitT1,
                request.PriceT1,
                request.PriceLimitT2,
                request.PriceT2,
                request.PriceM,
                request.PriceLimitM1,
                request.PriceM1,
                request.PriceLimitM2,
                request.PriceM2,
                request.NDS);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var priceId = await _priceService.UpdatePrice(price);

            return Ok(priceId);
        }

        [HttpDelete("nomenclature/{nomenclatureId:guid}/stock/{stockId:guid}")]
        public async Task<ActionResult<Guid>> DeletePrice(Guid nomenclatureId, Guid stockId)
        {
            var priceId = await _priceService.DeletePrice(nomenclatureId, stockId);

            return Ok(priceId);
        }
    }
}