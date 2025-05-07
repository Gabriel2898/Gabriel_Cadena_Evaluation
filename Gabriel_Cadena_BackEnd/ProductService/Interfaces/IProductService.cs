using ProductService.DTOs;
using ProductService.Helpers;

namespace ProductService.Interfaces
{
    public interface IProductService
    {
        Task<PagedResponse<ProductDTO>> GetProductsAsync(PaginationFilter paginationFilter);
        Task<ProductDTO> GetProductByIdAsync(int id);
        Task<ProductDTO> CreateProductAsync(CreateProductDTO productDto);
        Task<bool> UpdateProductAsync(UpdateProductDTO productDto);
        Task<bool> DeleteProductAsync(int id);
        Task<bool> UpdateStockAsync(int productId, int quantity);
    }
}