using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IPipeTypeService
    {
        Task<List<PipeType>> GetAllPipeTypes();
        Task<PipeType?> GetPipeTypeById(Guid id);
        Task<List<PipeType>> GetPipeTypesByParentId(Guid? parentId);
        Task<List<PipeType>> GetPipeTypesByName(string typeName);
        Task<Guid> CreatePipeType(PipeType pipeType);
        Task<Guid> UpdatePipeType(PipeType pipeType);
        Task<Guid> DeletePipeType(Guid id);
    }
}