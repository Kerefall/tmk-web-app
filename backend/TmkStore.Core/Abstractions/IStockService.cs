using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IStockService
    {
        Task<List<Stock>> GetAllStocks();
        Task<Stock?> GetStockById(Guid id);
        Task<List<Stock>> SearchStocks(string? city, string? stockName);
        Task<Guid> CreateStock(Stock stock);
        Task<Guid> UpdateStock(Stock stock);
        Task<Guid> DeleteStock(Guid id);
    }
}