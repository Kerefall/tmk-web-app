using Microsoft.AspNetCore.Mvc;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PipeTypeController : ControllerBase
    {
        private readonly IPipeTypeService _pipeTypeService;

        public PipeTypeController(IPipeTypeService pipeTypeService)
        {
            _pipeTypeService = pipeTypeService;
        }

        [HttpGet]
        public async Task<ActionResult<List<PipeTypeResponse>>> GetPipeTypes()
        {
            var pipeTypes = await _pipeTypeService.GetAllPipeTypes();

            var response = pipeTypes.Select(pt => new PipeTypeResponse(
                pt.IDType,
                pt.Type,
                pt.IDParentType));

            return Ok(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<PipeTypeResponse>> GetPipeTypeById(Guid id)
        {
            var pipeType = await _pipeTypeService.GetPipeTypeById(id);

            if (pipeType == null)
            {
                return NotFound();
            }

            var response = new PipeTypeResponse(
                pipeType.IDType,
                pipeType.Type,
                pipeType.IDParentType);

            return Ok(response);
        }

        [HttpGet("parent/{parentId:guid}")]
        public async Task<ActionResult<List<PipeTypeResponse>>> GetPipeTypesByParentId(Guid parentId)
        {
            var pipeTypes = await _pipeTypeService.GetPipeTypesByParentId(parentId);

            var response = pipeTypes.Select(pt => new PipeTypeResponse(
                pt.IDType,
                pt.Type,
                pt.IDParentType));

            return Ok(response);
        }

        [HttpGet("root")]
        public async Task<ActionResult<List<PipeTypeResponse>>> GetRootPipeTypes()
        {
            var pipeTypes = await _pipeTypeService.GetPipeTypesByParentId(null);

            var response = pipeTypes.Select(pt => new PipeTypeResponse(
                pt.IDType,
                pt.Type,
                pt.IDParentType));

            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<ActionResult<List<PipeTypeResponse>>> SearchPipeTypes([FromQuery] string typeName)
        {
            var pipeTypes = await _pipeTypeService.GetPipeTypesByName(typeName);

            var response = pipeTypes.Select(pt => new PipeTypeResponse(
                pt.IDType,
                pt.Type,
                pt.IDParentType));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreatePipeType([FromBody] PipeTypeRequest request)
        {
            var (pipeType, error) = PipeType.Create(
                Guid.NewGuid(),
                request.Type,
                request.IDParentType);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var pipeTypeId = await _pipeTypeService.CreatePipeType(pipeType);

            return Ok(pipeTypeId);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<Guid>> UpdatePipeType(Guid id, [FromBody] PipeTypeRequest request)
        {
            var (pipeType, error) = PipeType.Create(
                id,
                request.Type,
                request.IDParentType);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var pipeTypeId = await _pipeTypeService.UpdatePipeType(pipeType);

            return Ok(pipeTypeId);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<Guid>> DeletePipeType(Guid id)
        {
            try
            {
                var pipeTypeId = await _pipeTypeService.DeletePipeType(id);
                return Ok(pipeTypeId);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}