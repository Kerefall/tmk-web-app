using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class NomenclatureRepository : INomenclatureRepository
    {
        private readonly TmkStoreDbContext _context;

        public NomenclatureRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Nomenclature>> Get()
        {
            var nomenclatureEntities = await _context.Nomenclatures
                .AsNoTracking()
                .ToListAsync();

            var nomenclatures = nomenclatureEntities
                .Select(n => Nomenclature.Create(
                    n.ID,
                    n.IDCat,
                    n.IDType,
                    n.IDTypeNew,
                    n.ProductionType,
                    n.IDFunctionType,
                    n.Name,
                    n.Gost,
                    n.FormOfLength,
                    n.Manufacturer,
                    n.SteelGrade,
                    n.Diameter,
                    n.ProfileSize2,
                    n.PipeWallThickness,
                    n.Status,
                    n.Koef).Nomenclature)
                .ToList();

            return nomenclatures;
        }

        public async Task<Guid> Create(Nomenclature nomenclature)
        {
            var nomenclatureEntity = new NomenclatureEntity
            {
                ID = nomenclature.ID,
                IDCat = nomenclature.IDCat,
                IDType = nomenclature.IDType,
                IDTypeNew = nomenclature.IDTypeNew,
                ProductionType = nomenclature.ProductionType,
                IDFunctionType = nomenclature.IDFunctionType,
                Name = nomenclature.Name,
                Gost = nomenclature.Gost,
                FormOfLength = nomenclature.FormOfLength,
                Manufacturer = nomenclature.Manufacturer,
                SteelGrade = nomenclature.SteelGrade,
                Diameter = nomenclature.Diameter,
                ProfileSize2 = nomenclature.ProfileSize2,
                PipeWallThickness = nomenclature.PipeWallThickness,
                Status = nomenclature.Status,
                Koef = nomenclature.Koef
            };

            await _context.Nomenclatures.AddAsync(nomenclatureEntity);
            await _context.SaveChangesAsync();

            return nomenclatureEntity.ID;
        }

        public async Task<Guid> Update(Nomenclature nomenclature)
        {
            await _context.Nomenclatures
                .Where(n => n.ID == nomenclature.ID)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(n => n.IDCat, n => nomenclature.IDCat)
                    .SetProperty(n => n.IDType, n => nomenclature.IDType)
                    .SetProperty(n => n.IDTypeNew, n => nomenclature.IDTypeNew)
                    .SetProperty(n => n.ProductionType, n => nomenclature.ProductionType)
                    .SetProperty(n => n.IDFunctionType, n => nomenclature.IDFunctionType)
                    .SetProperty(n => n.Name, n => nomenclature.Name)
                    .SetProperty(n => n.Gost, n => nomenclature.Gost)
                    .SetProperty(n => n.FormOfLength, n => nomenclature.FormOfLength)
                    .SetProperty(n => n.Manufacturer, n => nomenclature.Manufacturer)
                    .SetProperty(n => n.SteelGrade, n => nomenclature.SteelGrade)
                    .SetProperty(n => n.Diameter, n => nomenclature.Diameter)
                    .SetProperty(n => n.ProfileSize2, n => nomenclature.ProfileSize2)
                    .SetProperty(n => n.PipeWallThickness, n => nomenclature.PipeWallThickness)
                    .SetProperty(n => n.Status, n => nomenclature.Status)
                    .SetProperty(n => n.Koef, n => nomenclature.Koef));

            return nomenclature.ID;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Nomenclatures
                .Where(n => n.ID == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<Nomenclature?> GetById(Guid id)
        {
            var nomenclatureEntity = await _context.Nomenclatures
                .AsNoTracking()
                .FirstOrDefaultAsync(n => n.ID == id);

            if (nomenclatureEntity == null)
                return null;

            var (nomenclature, error) = Nomenclature.Create(
                nomenclatureEntity.ID,
                nomenclatureEntity.IDCat,
                nomenclatureEntity.IDType,
                nomenclatureEntity.IDTypeNew,
                nomenclatureEntity.ProductionType,
                nomenclatureEntity.IDFunctionType,
                nomenclatureEntity.Name,
                nomenclatureEntity.Gost,
                nomenclatureEntity.FormOfLength,
                nomenclatureEntity.Manufacturer,
                nomenclatureEntity.SteelGrade,
                nomenclatureEntity.Diameter,
                nomenclatureEntity.ProfileSize2,
                nomenclatureEntity.PipeWallThickness,
                nomenclatureEntity.Status,
                nomenclatureEntity.Koef);

            return string.IsNullOrEmpty(error) ? nomenclature : null;
        }
    }
}