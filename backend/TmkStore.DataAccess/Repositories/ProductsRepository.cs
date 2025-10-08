using Microsoft.EntityFrameworkCore;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStore.DataAccess.Entities;

namespace TmkStore.DataAccess.Repositories
{
    public class ProductsRepository : IProductsRepository
    {
        private readonly TmkStoreDbContext _context;

        public ProductsRepository(TmkStoreDbContext context)
        {
            _context = context;
        }

        public async Task<List<Product>> Get()
        {
            var productEntities = await _context.Products
                .AsNoTracking()
                .ToListAsync();

            var products = productEntities
                .Select(p => Product.Create(p.Id, p.Title, p.Description, p.Price).Product)
                .ToList();

            return products;
        }

        public async Task<Guid> Create(Product product)
        {
            var productEntity = new ProductEntity
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price
            };

            await _context.Products.AddAsync(productEntity);
            await _context.SaveChangesAsync();

            return productEntity.Id;
        }

        public async Task<Guid> Update(Guid id, string title, string description, decimal price)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Title, p => title)
                    .SetProperty(p => p.Description, p => description)
                    .SetProperty(p => p.Price, p => price));

            return id;
        }

        public async Task<Guid> Delete(Guid id)
        {
            await _context.Products
                .Where(p => p.Id == id)
                .ExecuteDeleteAsync();

            return id;
        }
    }
}
