using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IRemnantRepository
    {
        Task<List<Remnant>> Get();
        Task<List<Remnant>> GetByNomenclatureId(Guid nomenclatureId);
        Task<List<Remnant>> GetByStockId(Guid stockId);
        Task<Remnant?> GetByNomenclatureAndStock(Guid nomenclatureId, Guid stockId);
        Task<Guid> Create(Remnant remnant);
        Task<Guid> Update(Remnant remnant);
        Task<Guid> Delete(Guid nomenclatureId, Guid stockId);
        Task DeleteByNomenclature(Guid nomenclatureId);
        Task DeleteByStock(Guid stockId);
    }
}