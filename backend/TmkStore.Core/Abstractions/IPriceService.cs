using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IPriceService
    {
        Task<List<Price>> GetAllPrices();
        Task<List<Price>> GetPricesByNomenclatureId(Guid nomenclatureId);
        Task<List<Price>> GetPricesByStockId(Guid stockId);
        Task<Price?> GetPriceByNomenclatureAndStock(Guid nomenclatureId, Guid stockId);
        Task<Guid> CreatePrice(Price price);
        Task<Guid> UpdatePrice(Price price);
        Task<Guid> DeletePrice(Guid nomenclatureId, Guid stockId);
    }
}