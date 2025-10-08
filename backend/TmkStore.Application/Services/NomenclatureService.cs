using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Repositories;

namespace TmkStore.Application.Services
{
    public class NomenclatureService : INomenclatureService
    {
        private readonly INomenclatureRepository _nomenclatureRepository;

        public NomenclatureService(INomenclatureRepository nomenclatureRepository)
        {
            _nomenclatureRepository = nomenclatureRepository;
        }

        public async Task<List<Nomenclature>> GetAllNomenclatures()
        {
            return await _nomenclatureRepository.Get();
        }

        public async Task<Nomenclature?> GetNomenclatureById(Guid id)
        {
            return await _nomenclatureRepository.GetById(id);
        }

        public async Task<Guid> CreateNomenclature(Nomenclature nomenclature)
        {
            return await _nomenclatureRepository.Create(nomenclature);
        }

        public async Task<Guid> UpdateNomenclature(Nomenclature nomenclature)
        {
            return await _nomenclatureRepository.Update(nomenclature);
        }

        public async Task<Guid> DeleteNomenclature(Guid id)
        {
            return await _nomenclatureRepository.Delete(id);
        }
    }
}
