using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;

namespace TmkStore.Application.Services
{
    public class ProductsService : IProductsService
    {
        private readonly IProductsRepository _productsRepository;

        public ProductsService(IProductsRepository productsRepository)
        {
            _productsRepository = productsRepository;
        }

        public async Task<List<Product>> GetAllProducts()
        {
            return await _productsRepository.Get();
        }

        public async Task<Guid> CreateProduct(Product product)
        {
            return await _productsRepository.Create(product);
        }

        public async Task<Guid> UpdateProduct(Guid id, string title, string description, decimal price)
        {
            return await _productsRepository.Update(id, title, description, price);
        }

        public async Task<Guid> DeleteProduct(Guid id)
        {
            return await _productsRepository.Delete(id);
        }
    }
}
