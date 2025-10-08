using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class StockRepository : IStockRepository
    {
        private readonly TmkStoreDbContext _context;

        public StockRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> Get()
        {
            var stockEntities = await _context.Stocks
                .AsNoTracking()
                .ToListAsync();

            var stocks = stockEntities
                .Select(s => Stock.Create(s.IDStock, s.City, s.StockName).Stock)
                .ToList();

            return stocks;
        }

        public async Task<Stock?> GetById(Guid id)
        {
            var stockEntity = await _context.Stocks
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.IDStock == id);

            if (stockEntity == null)
                return null;

            var (stock, error) = Stock.Create(
                stockEntity.IDStock,
                stockEntity.City,
                stockEntity.StockName);

            return string.IsNullOrEmpty(error) ? stock : null;
        }

        public async Task<List<Stock>> GetByCity(string city)
        {
            var stockEntities = await _context.Stocks
                .AsNoTracking()
                .Where(s => s.City.Contains(city))
                .ToListAsync();

            var stocks = stockEntities
                .Select(s => Stock.Create(s.IDStock, s.City, s.StockName).Stock)
                .ToList();

            return stocks;
        }

        public async Task<List<Stock>> GetByName(string stockName)
        {
            var stockEntities = await _context.Stocks
                .AsNoTracking()
                .Where(s => s.StockName.Contains(stockName))
                .ToListAsync();

            var stocks = stockEntities
                .Select(s => Stock.Create(s.IDStock, s.City, s.StockName).Stock)
                .ToList();

            return stocks;
        }

        public async Task<Guid> Create(Stock stock)
        {
            var stockEntity = new StockEntity
            {
                IDStock = stock.IDStock,
                City = stock.City,
                StockName = stock.StockName
            };

            await _context.Stocks.AddAsync(stockEntity);
            await _context.SaveChangesAsync();

            return stockEntity.IDStock;
        }

        public async Task<Guid> Update(Stock stock)
        {
            await _context.Stocks
                .Where(s => s.IDStock == stock.IDStock)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(st => st.City, st => stock.City)
                    .SetProperty(st => st.StockName, st => stock.StockName));

            return stock.IDStock;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Stocks
                .Where(s => s.IDStock == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.Stocks
                .AsNoTracking()
                .AnyAsync(s => s.IDStock == id);
        }
    }
}