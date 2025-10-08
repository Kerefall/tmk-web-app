using TmkStore.Core.Models;

namespace TmkStore.DataAccess.Repositories
{
    public interface INomenclatureRepository
    {
        Task<Guid> Create(Nomenclature nomenclature);
        Task<Guid> Delete(Guid id);
        Task<List<Nomenclature>> Get();
        Task<Nomenclature?> GetById(Guid id);
        Task<Guid> Update(Nomenclature nomenclature);
    }
}