using Microsoft.AspNetCore.Mvc;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RemnantController : ControllerBase
    {
        private readonly IRemnantService _remnantService;

        public RemnantController(IRemnantService remnantService)
        {
            _remnantService = remnantService;
        }

        [HttpGet]
        public async Task<ActionResult<List<RemnantResponse>>> GetRemnants()
        {
            var remnants = await _remnantService.GetAllRemnants();

            var response = remnants.Select(r => new RemnantResponse(
                r.ID,
                r.IDStock,
                r.InStockT,
                r.InStockM,
                r.AvgTubeLength,
                r.AvgTubeWeight));

            return Ok(response);
        }

        [HttpGet("nomenclature/{nomenclatureId:guid}")]
        public async Task<ActionResult<List<RemnantResponse>>> GetRemnantsByNomenclature(Guid nomenclatureId)
        {
            var remnants = await _remnantService.GetRemnantsByNomenclatureId(nomenclatureId);

            var response = remnants.Select(r => new RemnantResponse(
                r.ID,
                r.IDStock,
                r.InStockT,
                r.InStockM,
                r.AvgTubeLength,
                r.AvgTubeWeight));

            return Ok(response);
        }

        [HttpGet("stock/{stockId:guid}")]
        public async Task<ActionResult<List<RemnantResponse>>> GetRemnantsByStock(Guid stockId)
        {
            var remnants = await _remnantService.GetRemnantsByStockId(stockId);

            var response = remnants.Select(r => new RemnantResponse(
                r.ID,
                r.IDStock,
                r.InStockT,
                r.InStockM,
                r.AvgTubeLength,
                r.AvgTubeWeight));

            return Ok(response);
        }

        [HttpGet("nomenclature/{nomenclatureId:guid}/stock/{stockId:guid}")]
        public async Task<ActionResult<RemnantResponse>> GetRemnantByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            var remnant = await _remnantService.GetRemnantByNomenclatureAndStock(nomenclatureId, stockId);

            if (remnant == null)
            {
                return NotFound();
            }

            var response = new RemnantResponse(
                remnant.ID,
                remnant.IDStock,
                remnant.InStockT,
                remnant.InStockM,
                remnant.AvgTubeLength,
                remnant.AvgTubeWeight);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateRemnant([FromBody] RemnantRequest request)
        {
            var (remnant, error) = Remnant.Create(
                request.ID,
                request.IDStock,
                request.InStockT,
                request.InStockM,
                request.AvgTubeLength,
                request.AvgTubeWeight);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var remnantId = await _remnantService.CreateRemnant(remnant);

            return Ok(remnantId);
        }

        [HttpPut]
        public async Task<ActionResult<Guid>> UpdateRemnant([FromBody] RemnantRequest request)
        {
            var (remnant, error) = Remnant.Create(
                request.ID,
                request.IDStock,
                request.InStockT,
                request.InStockM,
                request.AvgTubeLength,
                request.AvgTubeWeight);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var remnantId = await _remnantService.UpdateRemnant(remnant);

            return Ok(remnantId);
        }

        [HttpDelete("nomenclature/{nomenclatureId:guid}/stock/{stockId:guid}")]
        public async Task<ActionResult<Guid>> DeleteRemnant(Guid nomenclatureId, Guid stockId)
        {
            var remnantId = await _remnantService.DeleteRemnant(nomenclatureId, stockId);

            return Ok(remnantId);
        }
    }
}