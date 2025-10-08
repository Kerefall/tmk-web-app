using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;

namespace TmkStore.Application.Services
{
    public class PipeTypeService : IPipeTypeService
    {
        private readonly IPipeTypeRepository _pipeTypeRepository;

        public PipeTypeService(IPipeTypeRepository pipeTypeRepository)
        {
            _pipeTypeRepository = pipeTypeRepository;
        }

        public async Task<List<PipeType>> GetAllPipeTypes()
        {
            return await _pipeTypeRepository.Get();
        }

        public async Task<PipeType?> GetPipeTypeById(Guid id)
        {
            return await _pipeTypeRepository.GetById(id);
        }

        public async Task<List<PipeType>> GetPipeTypesByParentId(Guid? parentId)
        {
            return await _pipeTypeRepository.GetByParentId(parentId);
        }

        public async Task<List<PipeType>> GetPipeTypesByName(string typeName)
        {
            return await _pipeTypeRepository.GetByName(typeName);
        }

        public async Task<Guid> CreatePipeType(PipeType pipeType)
        {
            return await _pipeTypeRepository.Create(pipeType);
        }

        public async Task<Guid> UpdatePipeType(PipeType pipeType)
        {
            return await _pipeTypeRepository.Update(pipeType);
        }

        public async Task<Guid> DeletePipeType(Guid id)
        {
            return await _pipeTypeRepository.Delete(id);
        }
    }
}