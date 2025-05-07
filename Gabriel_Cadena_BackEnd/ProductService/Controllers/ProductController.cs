using Microsoft.AspNetCore.Mvc;
using ProductService.DTOs;
using ProductService.Helpers;
using ProductService.Interfaces;

namespace ProductService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<ProductDTO>>> GetProducts(
            [FromQuery] PaginationFilter paginationFilter)
        {
            var response = await _productService.GetProductsAsync(paginationFilter);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult<ProductDTO>> CreateProduct(CreateProductDTO productDto)
        {
            var createdProduct = await _productService.CreateProductAsync(productDto);
            return CreatedAtAction(nameof(GetProduct), new { id = createdProduct.Id }, createdProduct);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, UpdateProductDTO productDto)
        {
            if (id != productDto.Id) return BadRequest();

            var result = await _productService.UpdateProductAsync(productDto);
            if (!result) return NotFound();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var result = await _productService.DeleteProductAsync(id);
            if (!result) return NotFound();

            return NoContent();
        }
        [HttpPut("{id}/stock")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateStock(int id, [FromBody] UpdateStockDTO updateStockDto)
        {
            try
            {
                var result = await _productService.UpdateStockAsync(id, updateStockDto.Quantity);

                if (!result)
                    return BadRequest(new { Message = "No se pudo actualizar el stock. Verifique el ID del producto o la cantidad." });

                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { Message = "Error interno al actualizar el stock", Error = ex.Message });
            }
        }
    }
}