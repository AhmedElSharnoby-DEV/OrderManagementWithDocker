using IOrderManagementSystem.Services.Abstractions;
using Microsoft.AspNetCore.Mvc;

namespace IOrderManagementSystem.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        [HttpGet("get-all-products")]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await _productService.GetAllProductsAsync());
        }
        [HttpGet("get-product-by-id")]
        public async Task<IActionResult> GetProductById([FromQuery] long id)
        {
            return Ok(await _productService.GetProductByIdAsync(id));
        }
    }
}
