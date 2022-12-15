using Microsoft.AspNetCore.Mvc;
using Net7.WebApi.Test.Services.Abstractions;

namespace Net7.WebApi.Test.Controllers
{
    [ApiController]
    [Route("api/teapots")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService) 
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<TeapotResponse>>> GetProductsAsync()
        {
            var products = await _productService.GetProductsAsync();

            if (products == null || products.Count == 0)
            {
                return NotFound("There are no products!");
            }

            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TeapotResponse>> GetProductByIdAsync([FromRoute] int id)
        {
            var product = await _productService.GetProductByIdAsync(id);

            if (product == null)
            {
                return NotFound("Product with this id doesn't exist!");
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<bool>> AddProductAsync([FromBody] Teapot product)
        {
            bool isAdded = await _productService.AddProductAsync(product);

            if (!isAdded)
                return BadRequest("The product hasn't been added!");

            return Ok("The product has been added!");
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<bool>> UpdateProductAsync([FromRoute] int id, [FromBody] Teapot product)
        {
            bool isUpdated = await _productService.EditProductAsync(id, product);

            if (!isUpdated)
                return BadRequest("The product hasn't been updated!");

            return Ok("The product has been updated!");
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<bool>> DeleteProductByIdAsync([FromRoute] int id)
        {
            bool isRemoved = await _productService.DeleteProductByIdAsync(id);

            if (!isRemoved)
                return BadRequest("The product hasn't been removed!");

            return Ok("The product has been removed!");
        }
    }
}