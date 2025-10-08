using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class RemnantRepository : IRemnantRepository
    {
        private readonly TmkStoreDbContext _context;

        public RemnantRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Remnant>> Get()
        {
            var remnantEntities = await _context.Remnants
                .AsNoTracking()
                .ToListAsync();

            var remnants = remnantEntities
                .Select(r => Remnant.Create(
                    r.ID,
                    r.IDStock,
                    r.InStockT,
                    r.InStockM,
                    r.AvgTubeLength,
                    r.AvgTubeWeight).Remnant)
                .ToList();

            return remnants;
        }

        public async Task<List<Remnant>> GetByNomenclatureId(Guid nomenclatureId)
        {
            var remnantEntities = await _context.Remnants
                .AsNoTracking()
                .Where(r => r.ID == nomenclatureId)
                .ToListAsync();

            var remnants = remnantEntities
                .Select(r => Remnant.Create(
                    r.ID,
                    r.IDStock,
                    r.InStockT,
                    r.InStockM,
                    r.AvgTubeLength,
                    r.AvgTubeWeight).Remnant)
                .ToList();

            return remnants;
        }

        public async Task<List<Remnant>> GetByStockId(Guid stockId)
        {
            var remnantEntities = await _context.Remnants
                .AsNoTracking()
                .Where(r => r.IDStock == stockId)
                .ToListAsync();

            var remnants = remnantEntities
                .Select(r => Remnant.Create(
                    r.ID,
                    r.IDStock,
                    r.InStockT,
                    r.InStockM,
                    r.AvgTubeLength,
                    r.AvgTubeWeight).Remnant)
                .ToList();

            return remnants;
        }

        public async Task<Remnant?> GetByNomenclatureAndStock(Guid nomenclatureId, Guid stockId)
        {
            var remnantEntity = await _context.Remnants
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.ID == nomenclatureId && r.IDStock == stockId);

            if (remnantEntity == null)
                return null;

            var (remnant, error) = Remnant.Create(
                remnantEntity.ID,
                remnantEntity.IDStock,
                remnantEntity.InStockT,
                remnantEntity.InStockM,
                remnantEntity.AvgTubeLength,
                remnantEntity.AvgTubeWeight);

            return string.IsNullOrEmpty(error) ? remnant : null;
        }

        public async Task<Guid> Create(Remnant remnant)
        {
            var remnantEntity = new RemnantEntity
            {
                ID = remnant.ID,
                IDStock = remnant.IDStock,
                InStockT = remnant.InStockT,
                InStockM = remnant.InStockM,
                AvgTubeLength = remnant.AvgTubeLength,
                AvgTubeWeight = remnant.AvgTubeWeight
            };

            await _context.Remnants.AddAsync(remnantEntity);
            await _context.SaveChangesAsync();

            return remnantEntity.ID;
        }

        public async Task<Guid> Update(Remnant remnant)
        {
            await _context.Remnants
                .Where(r => r.ID == remnant.ID && r.IDStock == remnant.IDStock)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(r => r.InStockT, r => remnant.InStockT)
                    .SetProperty(r => r.InStockM, r => remnant.InStockM)
                    .SetProperty(r => r.AvgTubeLength, r => remnant.AvgTubeLength)
                    .SetProperty(r => r.AvgTubeWeight, r => remnant.AvgTubeWeight));

            return remnant.ID;
        }

        public async Task<Guid> Delete(Guid nomenclatureId, Guid stockId)
        {
            await _context.Remnants
                .Where(r => r.ID == nomenclatureId && r.IDStock == stockId)
                .ExecuteDeleteAsync();

            return nomenclatureId;
        }

        public async Task DeleteByNomenclature(Guid nomenclatureId)
        {
            await _context.Remnants
                .Where(r => r.ID == nomenclatureId)
                .ExecuteDeleteAsync();
        }

        public async Task DeleteByStock(Guid stockId)
        {
            await _context.Remnants
                .Where(r => r.IDStock == stockId)
                .ExecuteDeleteAsync();
        }
    }
}