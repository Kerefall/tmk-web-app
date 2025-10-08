using Microsoft.AspNetCore.Mvc;
using TmkStore.Application.Services;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NomenclatureController : ControllerBase
    {
        private readonly INomenclatureService _nomenclatureService;

        public NomenclatureController(INomenclatureService nomenclatureService)
        {
            _nomenclatureService = nomenclatureService;
        }

        [HttpGet]
        public async Task<ActionResult<List<NomenclatureResponse>>> GetNomenclatures()
        {
            var nomenclatures = await _nomenclatureService.GetAllNomenclatures();

            var response = nomenclatures.Select(n => new NomenclatureResponse(
                n.ID,
                n.IDCat,
                n.IDType,
                n.IDTypeNew,
                n.ProductionType,
                n.IDFunctionType,
                n.Name,
                n.Gost,
                n.FormOfLength,
                n.Manufacturer,
                n.SteelGrade,
                n.Diameter,
                n.ProfileSize2,
                n.PipeWallThickness,
                n.Status,
                n.Koef));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<NomenclatureResponse>> GetNomenclatureById(Guid id)
        {
            var nomenclature = await _nomenclatureService.GetNomenclatureById(id);

            if (nomenclature == null)
            {
                return NotFound();
            }

            var response = new NomenclatureResponse(
                nomenclature.ID,
                nomenclature.IDCat,
                nomenclature.IDType,
                nomenclature.IDTypeNew,
                nomenclature.ProductionType,
                nomenclature.IDFunctionType,
                nomenclature.Name,
                nomenclature.Gost,
                nomenclature.FormOfLength,
                nomenclature.Manufacturer,
                nomenclature.SteelGrade,
                nomenclature.Diameter,
                nomenclature.ProfileSize2,
                nomenclature.PipeWallThickness,
                nomenclature.Status,
                nomenclature.Koef);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateNomenclature([FromBody] NomenclatureRequest request)
        {
            var (nomenclature, error) = Nomenclature.Create(
                Guid.NewGuid(),
                request.IDCat,
                request.IDType,
                request.IDTypeNew,
                request.ProductionType,
                request.IDFunctionType,
                request.Name,
                request.Gost,
                request.FormOfLength,
                request.Manufacturer,
                request.SteelGrade,
                request.Diameter,
                request.ProfileSize2,
                request.PipeWallThickness,
                request.Status,
                request.Koef);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var nomenclatureId = await _nomenclatureService.CreateNomenclature(nomenclature);

            return Ok(nomenclatureId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdateNomenclature(Guid id, [FromBody] NomenclatureRequest request)
        {
            var (nomenclature, error) = Nomenclature.Create(
                id,
                request.IDCat,
                request.IDType,
                request.IDTypeNew,
                request.ProductionType,
                request.IDFunctionType,
                request.Name,
                request.Gost,
                request.FormOfLength,
                request.Manufacturer,
                request.SteelGrade,
                request.Diameter,
                request.ProfileSize2,
                request.PipeWallThickness,
                request.Status,
                request.Koef);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var nomenclatureId = await _nomenclatureService.UpdateNomenclature(nomenclature);

            return Ok(nomenclatureId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeleteNomenclature(Guid id)
        {
            var nomenclatureId = await _nomenclatureService.DeleteNomenclature(id);

            return Ok(nomenclatureId);
        }
    }
}