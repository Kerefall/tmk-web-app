using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IStockRepository
    {
        Task<List<Stock>> Get();
        Task<Stock?> GetById(Guid id);
        Task<List<Stock>> GetByCity(string city);
        Task<List<Stock>> GetByName(string stockName);
        Task<Guid> Create(Stock stock);
        Task<Guid> Update(Stock stock);
        Task<Guid> Delete(Guid id);
        Task<bool> Exists(Guid id);
    }
}