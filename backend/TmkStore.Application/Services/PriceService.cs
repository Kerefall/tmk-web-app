using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;

namespace TmkStore.Application.Services
{
    public class PriceService : IPriceService
    {
        private readonly IPriceRepository _priceRepository;

        public PriceService(IPriceRepository priceRepository)
        {
            _priceRepository = priceRepository;
        }

        public async Task<List<Price>> GetAllPrices()
        {
            return await _priceRepository.Get();
        }

        public async Task<List<Price>> GetPricesByNomenclatureId(Guid nomenclatureId)
        {
            return await _priceRepository.GetByNomenclatureId(nomenclatureId);
        }

        public async Task<List<Price>> GetPricesByStockId(Guid stockId)
        {
            return await _priceRepository.GetByStockId(stockId);
        }

        public async Task<Price?> GetPriceByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            return await _priceRepository.GetByNomenclatureAndStock(nomenclatureId, stockId);
        }

        public async Task<Guid> CreatePrice(Price price)
        {
            return await _priceRepository.Create(price);
        }

        public async Task<Guid> UpdatePrice(Price price)
        {
            return await _priceRepository.Update(price);
        }

        public async Task<Guid> DeletePrice(Guid nomenclatureId, Guid stockId)
        {
            return await _priceRepository.Delete(nomenclatureId, stockId);
        }
    }
}