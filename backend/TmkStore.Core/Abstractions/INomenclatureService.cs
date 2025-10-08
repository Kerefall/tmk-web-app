using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface INomenclatureService
    {
        Task<List<Nomenclature>> GetAllNomenclatures();
        Task<Nomenclature?> GetNomenclatureById(Guid id);
        Task<Guid> CreateNomenclature(Nomenclature nomenclature);
        Task<Guid> UpdateNomenclature(Nomenclature nomenclature);
        Task<Guid> DeleteNomenclature(Guid id);
    }
}