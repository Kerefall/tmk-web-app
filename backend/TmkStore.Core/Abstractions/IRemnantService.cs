using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IRemnantService
    {
        Task<List<Remnant>> GetAllRemnants();
        Task<List<Remnant>> GetRemnantsByNomenclatureId(Guid nomenclatureId);
        Task<List<Remnant>> GetRemnantsByStockId(Guid stockId);
        Task<Remnant?> GetRemnantByNomenclatureAndStock(Guid nomenclatureId, Guid stockId);
        Task<Guid> CreateRemnant(Remnant remnant);
        Task<Guid> UpdateRemnant(Remnant remnant);
        Task<Guid> DeleteRemnant(Guid nomenclatureId, Guid stockId);
    }
}