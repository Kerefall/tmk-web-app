using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class PipeTypeRepository : IPipeTypeRepository
    {
        private readonly TmkStoreDbContext _context;

        public PipeTypeRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<PipeType>> Get()
        {
            var pipeTypeEntities = await _context.PipeTypes
                .AsNoTracking()
                .ToListAsync();

            var pipeTypes = pipeTypeEntities
                .Select(pt => PipeType.Create(pt.IDType, pt.Type, pt.IDParentType).PipeType)
                .ToList();

            return pipeTypes;
        }

        public async Task<PipeType?> GetById(Guid id)
        {
            var pipeTypeEntity = await _context.PipeTypes
                .AsNoTracking()
                .FirstOrDefaultAsync(pt => pt.IDType == id);

            if (pipeTypeEntity == null)
                return null;

            var (pipeType, error) = PipeType.Create(
                pipeTypeEntity.IDType,
                pipeTypeEntity.Type,
                pipeTypeEntity.IDParentType);

            return string.IsNullOrEmpty(error) ? pipeType : null;
        }

        public async Task<List<PipeType>> GetByParentId(Guid? parentId)
        {
            var pipeTypeEntities = await _context.PipeTypes
                .AsNoTracking()
                .Where(pt => pt.IDParentType == parentId)
                .ToListAsync();

            var pipeTypes = pipeTypeEntities
                .Select(pt => PipeType.Create(pt.IDType, pt.Type, pt.IDParentType).PipeType)
                .ToList();

            return pipeTypes;
        }

        public async Task<List<PipeType>> GetByName(string typeName)
        {
            var pipeTypeEntities = await _context.PipeTypes
                .AsNoTracking()
                .Where(pt => pt.Type.Contains(typeName))
                .ToListAsync();

            var pipeTypes = pipeTypeEntities
                .Select(pt => PipeType.Create(pt.IDType, pt.Type, pt.IDParentType).PipeType)
                .ToList();

            return pipeTypes;
        }

        public async Task<Guid> Create(PipeType pipeType)
        {
            var pipeTypeEntity = new PipeTypeEntity
            {
                IDType = pipeType.IDType,
                Type = pipeType.Type,
                IDParentType = pipeType.IDParentType
            };

            await _context.PipeTypes.AddAsync(pipeTypeEntity);
            await _context.SaveChangesAsync();

            return pipeTypeEntity.IDType;
        }

        public async Task<Guid> Update(PipeType pipeType)
        {
            await _context.PipeTypes
                .Where(pt => pt.IDType == pipeType.IDType)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(pt => pt.Type, pt => pipeType.Type)
                    .SetProperty(pt => pt.IDParentType, pt => pipeType.IDParentType));

            return pipeType.IDType;
        }

        public async Task<Guid> Delete(Guid id)
        {
            // Проверяем, нет ли дочерних типов
            var hasChildren = await _context.PipeTypes
                .AnyAsync(pt => pt.IDParentType == id);

            if (hasChildren)
            {
                throw new InvalidOperationException("Нельзя удалить тип, у которого есть дочерние типы");
            }

            // Проверяем, не используется ли тип в номенклатуре
            var usedInNomenclature = await _context.Nomenclatures
                .AnyAsync(n => n.IDType == id);

            if (usedInNomenclature)
            {
                throw new InvalidOperationException("Нельзя удалить тип, который используется в номенклатуре");
            }

            await _context.PipeTypes
                .Where(pt => pt.IDType == id)
                .ExecuteDeleteAsync();

            return id;
        }

        public async Task<bool> Exists(Guid id)
        {
            return await _context.PipeTypes
                .AsNoTracking()
                .AnyAsync(pt => pt.IDType == id);
        }
    }
}