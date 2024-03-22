using CoptisTest.API.Repositories;
using CoptisTest.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoptisTest.API.Controllers
{
    [ApiController]
    [Route("Product/[action]")]
    public class ProductController : ControllerBase
    {
        private readonly ILogger<ProductController> _logger;
        private readonly IProductRepository _productRepository;

        public ProductController(ILogger<ProductController> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<ActionResult> GetProducts()
        {
            try
            {
                return Ok(await _productRepository.GetProducts());
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpGet("{productId:int}")]
        public async Task<ActionResult<Product>> GetProduct(int productId)
        {
            try
            {
                var result = await _productRepository.GetProduct(productId);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPost]
        public async Task<ActionResult<Product>> AddProduct(Product product)
        {
            try
            {
                if (product == null) return BadRequest();

                await _productRepository.AddProduct(product);

                return CreatedAtAction(nameof(GetProduct), new { ProductId = product.ProductId }, null);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpPut("{productId:int}")]
        public async Task<ActionResult<Product>> UpdateUser(int productId, Product product)
        {
            try
            {
                if (productId != product.ProductId)
                    return BadRequest("User ID mismatch");

                var productToUpdate = await _productRepository.GetProduct(productId);

                if (productToUpdate == null)
                {
                    return NotFound($"Product with Id = {productId} not found");
                }
                await _productRepository.UpdateProduct(product);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
            }
        }
        [HttpDelete("{productId:int}")]
        public async Task<ActionResult<User>> DeleteProduct(int productId)
        {
            try
            {
                var productToDelete = await _productRepository.GetProduct(productId);

                if (productToDelete == null)
                {
                    return NotFound($"Product with Id = {productId} not found");
                }
                await _productRepository.DeleteProduct(productId);
                return NoContent();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
    }
}
