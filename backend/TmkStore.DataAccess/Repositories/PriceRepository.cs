using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class PriceRepository : IPriceRepository
    {
        private readonly TmkStoreDbContext _context;

        public PriceRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Price>> Get()
        {
            var priceEntities = await _context.Prices
                .AsNoTracking()
                .ToListAsync();

            var prices = priceEntities
                .Select(p => Price.Create(
                    p.ID,
                    p.IDStock,
                    p.PriceT,
                    p.PriceLimitT1,
                    p.PriceT1,
                    p.PriceLimitT2,
                    p.PriceT2,
                    p.PriceM,
                    p.PriceLimitM1,
                    p.PriceM1,
                    p.PriceLimitM2,
                    p.PriceM2,
                    p.NDS).Price)
                .ToList();

            return prices;
        }

        public async Task<List<Price>> GetByNomenclatureId(Guid nomenclatureId)
        {
            var priceEntities = await _context.Prices
                .AsNoTracking()
                .Where(p => p.ID == nomenclatureId)
                .ToListAsync();

            var prices = priceEntities
                .Select(p => Price.Create(
                    p.ID,
                    p.IDStock,
                    p.PriceT,
                    p.PriceLimitT1,
                    p.PriceT1,
                    p.PriceLimitT2,
                    p.PriceT2,
                    p.PriceM,
                    p.PriceLimitM1,
                    p.PriceM1,
                    p.PriceLimitM2,
                    p.PriceM2,
                    p.NDS).Price)
                .ToList();

            return prices;
        }

        public async Task<List<Price>> GetByStockId(Guid stockId)
        {
            var priceEntities = await _context.Prices
                .AsNoTracking()
                .Where(p => p.IDStock == stockId)
                .ToListAsync();

            var prices = priceEntities
                .Select(p => Price.Create(
                    p.ID,
                    p.IDStock,
                    p.PriceT,
                    p.PriceLimitT1,
                    p.PriceT1,
                    p.PriceLimitT2,
                    p.PriceT2,
                    p.PriceM,
                    p.PriceLimitM1,
                    p.PriceM1,
                    p.PriceLimitM2,
                    p.PriceM2,
                    p.NDS).Price)
                .ToList();

            return prices;
        }

        public async Task<Price?> GetByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            var priceEntity = await _context.Prices
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.ID == nomenclatureId && p.IDStock == stockId);

            if (priceEntity == null)
                return null;

            var (price, error) = Price.Create(
                priceEntity.ID,
                priceEntity.IDStock,
                priceEntity.PriceT,
                priceEntity.PriceLimitT1,
                priceEntity.PriceT1,
                priceEntity.PriceLimitT2,
                priceEntity.PriceT2,
                priceEntity.PriceM,
                priceEntity.PriceLimitM1,
                priceEntity.PriceM1,
                priceEntity.PriceLimitM2,
                priceEntity.PriceM2,
                priceEntity.NDS);

            return string.IsNullOrEmpty(error) ? price : null;
        }

        public async Task<Guid> Create(Price price)
        {
            var priceEntity = new PriceEntity
            {
                ID = price.ID,
                IDStock = price.IDStock,
                PriceT = price.PriceT,
                PriceLimitT1 = price.PriceLimitT1,
                PriceT1 = price.PriceT1,
                PriceLimitT2 = price.PriceLimitT2,
                PriceT2 = price.PriceT2,
                PriceM = price.PriceM,
                PriceLimitM1 = price.PriceLimitM1,
                PriceM1 = price.PriceM1,
                PriceLimitM2 = price.PriceLimitM2,
                PriceM2 = price.PriceM2,
                NDS = price.NDS
            };

            await _context.Prices.AddAsync(priceEntity);
            await _context.SaveChangesAsync();

            return priceEntity.ID;
        }

        public async Task<Guid> Update(Price price)
        {
            await _context.Prices
                .Where(p => p.ID == price.ID && p.IDStock == price.IDStock)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.PriceT, p => price.PriceT)
                    .SetProperty(p => p.PriceLimitT1, p => price.PriceLimitT1)
                    .SetProperty(p => p.PriceT1, p => price.PriceT1)
                    .SetProperty(p => p.PriceLimitT2, p => price.PriceLimitT2)
                    .SetProperty(p => p.PriceT2, p => price.PriceT2)
                    .SetProperty(p => p.PriceM, p => price.PriceM)
                    .SetProperty(p => p.PriceLimitM1, p => price.PriceLimitM1)
                    .SetProperty(p => p.PriceM1, p => price.PriceM1)
                    .SetProperty(p => p.PriceLimitM2, p => price.PriceLimitM2)
                    .SetProperty(p => p.PriceM2, p => price.PriceM2)
                    .SetProperty(p => p.NDS, p => price.NDS));

            return price.ID;
        }

        public async Task<Guid> Delete(Guid nomenclatureId, Guid stockId)
        {
            await _context.Prices
                .Where(p => p.ID == nomenclatureId && p.IDStock == stockId)
                .ExecuteDeleteAsync();

            return nomenclatureId;
        }

        public async Task DeleteByNomenclature(Guid nomenclatureId)
        {
            await _context.Prices
                .Where(p => p.ID == nomenclatureId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteByStock(Guid stockId)
        {
            await _context.Prices
                .Where(p => p.IDStock == stockId)
                .ExecuteDeleteAsync();
        }
    }
}