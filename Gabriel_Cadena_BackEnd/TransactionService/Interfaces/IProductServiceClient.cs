using TransactionService.DTOs;

namespace TransactionService.Interfaces
{
    public interface IProductServiceClient
    {
        Task<ProductDTO> GetProductAsync(int productId);
        Task<bool> UpdateProductStockAsync(int productId, int quantity);
    }
}