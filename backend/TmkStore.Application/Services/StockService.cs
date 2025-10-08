using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;

namespace TmkStore.Application.Services
{
    public class StockService : IStockService
    {
        private readonly IStockRepository _stockRepository;

        public StockService(IStockRepository stockRepository)
        {
            _stockRepository = stockRepository;
        }

        public async Task<List<Stock>> GetAllStocks()
        {
            return await _stockRepository.Get();
        }

        public async Task<Stock?> GetStockById(Guid id)
        {
            return await _stockRepository.GetById(id);
        }

        public async Task<List<Stock>> SearchStocks(string? city, string? stockName)
        {
            var allStocks = await _stockRepository.Get();

            var filtered = allStocks.AsEnumerable();

            if (!string.IsNullOrEmpty(city))
            {
                filtered = filtered.Where(s => s.City.Contains(city, StringComparison.OrdinalIgnoreCase));
            }

            if (!string.IsNullOrEmpty(stockName))
            {
                filtered = filtered.Where(s => s.StockName.Contains(stockName, StringComparison.OrdinalIgnoreCase));
            }

            return filtered.ToList();
        }

        public async Task<Guid> CreateStock(Stock stock)
        {
            return await _stockRepository.Create(stock);
        }

        public async Task<Guid> UpdateStock(Stock stock)
        {
            return await _stockRepository.Update(stock);
        }

        public async Task<Guid> DeleteStock(Guid id)
        {
            return await _stockRepository.Delete(id);
        }
    }
}