using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IProductsService
    {
        Task<Guid> CreateProduct(Product product);
        Task<Guid> DeleteProduct(Guid id);
        Task<List<Product>> GetAllProducts();
        Task<Guid> UpdateProduct(Guid id, string title, string description, decimal price);
    }
}