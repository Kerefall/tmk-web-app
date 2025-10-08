using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IPipeTypeRepository
    {
        Task<List<PipeType>> Get();
        Task<PipeType?> GetById(Guid id);
        Task<List<PipeType>> GetByParentId(Guid? parentId);
        Task<List<PipeType>> GetByName(string typeName);
        Task<Guid> Create(PipeType pipeType);
        Task<Guid> Update(PipeType pipeType);
        Task<Guid> Delete(Guid id);
        Task<bool> Exists(Guid id);
    }
}