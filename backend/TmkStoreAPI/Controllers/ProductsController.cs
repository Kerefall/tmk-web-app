using Microsoft.AspNetCore.Mvc;
using TmkStore.Core.Abstractions;
using TmkStore.Core.Models;
using TmkStoreAPI.Contracts;

namespace TmkStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsService _productsService;

        public ProductsController(IProductsService productsService)
        {
            _productsService = productsService;
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductsResponse>>> GetProducts()
        {
            var products = await _productsService.GetAllProducts();

            var response = products.Select(p => new ProductsResponse(p.Id, p.Title, p.Description, p.Price));

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> CreateProduct([FromBody] ProductsRequest request)
        {
            var (product, error) = Product.Create(
                Guid.NewGuid(),
                request.Title,
                request.Description,
                request.Price);

            if (!string.IsNullOrEmpty(error))
            {
                return BadRequest(error);
            }

            var productId = await _productsService.CreateProduct(product);

            return Ok(productId);
        }
    }
}
