using TmkStore.Core.Models;

namespace TmkStore.Core.Abstractions
{
    public interface IProductsRepository
    {
        Task<Guid> Create(Product product);
        Task<Guid> Delete(Guid id);
        Task<List<Product>> Get();
        Task<Guid> Update(Guid id, string title, string description, decimal price);
    }
}