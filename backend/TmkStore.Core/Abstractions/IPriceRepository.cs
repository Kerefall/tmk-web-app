using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IPriceRepository
    {
        Task<List<Price>> Get();
        Task<List<Price>> GetByNomenclatureId(Guid nomenclatureId);
        Task<List<Price>> GetByStockId(Guid stockId);
        Task<Price?> GetByNomenclatureAndStock(Guid nomenclatureId, Guid stockId);
        Task<Guid> Create(Price price);
        Task<Guid> Update(Price price);
        Task<Guid> Delete(Guid nomenclatureId, Guid stockId);
        Task DeleteByNomenclature(Guid nomenclatureId);
        Task DeleteByStock(Guid stockId);
    }
}