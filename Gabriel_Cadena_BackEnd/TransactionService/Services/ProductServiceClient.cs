using System.Net;
using System.Net.Http.Json;
using TransactionService.DTOs;
using TransactionService.Interfaces;

namespace TransactionService.Services
{
    public class ProductServiceClient : IProductServiceClient
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ProductServiceClient> _logger;

        public ProductServiceClient(HttpClient httpClient, ILogger<ProductServiceClient> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<ProductDTO> GetProductAsync(int productId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"/api/products/{productId}");

                if (response.StatusCode == HttpStatusCode.NotFound)
                    return null;

                if (!response.IsSuccessStatusCode)
                {
                    _logger.LogError($"Error al obtener producto: {response.StatusCode}");
                    return null;
                }

                return await response.Content.ReadFromJsonAsync<ProductDTO>();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al comunicarse con ProductService");
                return null;
            }
        }

        public async Task<bool> UpdateProductStockAsync(int productId, int quantity)
        {
            try
            {
                if (_httpClient == null || _httpClient.BaseAddress == null)
                {
                }
                    var response = await _httpClient.PutAsJsonAsync(
                    $"/api/products/{productId}/stock",
                    new { Quantity = quantity });

                if (!response.IsSuccessStatusCode)
                {
                    var errorContent = await response.Content.ReadAsStringAsync();
                    throw new HttpRequestException(
                        $"Error actualizando stock: {response.StatusCode} - {errorContent}");
                }

                return true;
            }
            catch (HttpRequestException)
            {
                throw; 
            }
        }
    }
}