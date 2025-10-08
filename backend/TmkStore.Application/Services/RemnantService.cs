using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;

namespace TmkStore.Application.Services
{
    public class RemnantService : IRemnantService
    {
        private readonly IRemnantRepository _remnantRepository;

        public RemnantService(IRemnantRepository remnantRepository)
        {
            _remnantRepository = remnantRepository;
        }

        public async Task<List<Remnant>> GetAllRemnants()
        {
            return await _remnantRepository.Get();
        }

        public async Task<List<Remnant>> GetRemnantsByNomenclatureId(Guid nomenclatureId)
        {
            return await _remnantRepository.GetByNomenclatureId(nomenclatureId);
        }

        public async Task<List<Remnant>> GetRemnantsByStockId(Guid stockId)
        {
            return await _remnantRepository.GetByStockId(stockId);
        }

        public async Task<Remnant?> GetRemnantByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            return await _remnantRepository.GetByNomenclatureAndStock(nomenclatureId, stockId);
        }

        public async Task<Guid> CreateRemnant(Remnant remnant)
        {
            return await _remnantRepository.Create(remnant);
        }

        public async Task<Guid> UpdateRemnant(Remnant remnant)
        {
            return await _remnantRepository.Update(remnant);
        }

        public async Task<Guid> DeleteRemnant(Guid nomenclatureId, Guid stockId)
        {
            return await _remnantRepository.Delete(nomenclatureId, stockId);
        }
    }
}