using Microsoft.AspNetCore.Mvc;
using ProductService.Helpers;
using TransactionService.DTOs;
using TransactionService.Interfaces;
using TransactionService.Models;

namespace TransactionService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IProductServiceClient _productServiceClient;

        public TransactionsController(
            ITransactionService transactionService,
            IProductServiceClient productServiceClient)
        {
            _transactionService = transactionService;
            _productServiceClient = productServiceClient;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResponse<TransactionDTO>>> GetTransactions(
            [FromQuery] PaginationFilter paginationFilter,
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            var response = await _transactionService.GetTransactionsAsync(
                paginationFilter, startDate, endDate);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<TransactionDTO>> CreateTransaction([FromBody] CreateTransactionDTO transactionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!Enum.TryParse<TransactionType>(transactionDto.TransactionType, out var transactionType))
                return BadRequest("Tipo de transacción no válido. Use 'Purchase' o 'Sale'");


            if (transactionType == TransactionType.Sale)
            {
                var product = await _productServiceClient.GetProductAsync(transactionDto.ProductId);
                if (product == null)
                    return BadRequest("Producto no encontrado");

                if (product.Stock < transactionDto.Quantity)
                    return BadRequest($"Stock insuficiente. Disponible: {product.Stock}, Solicitado: {transactionDto.Quantity}");
            }

            var transaction = await _transactionService.CreateTransactionAsync(transactionDto);

            var quantity = transactionType == TransactionType.Purchase
                ? transactionDto.Quantity
                : -transactionDto.Quantity;

            try
            {
                var stockUpdated = await _productServiceClient.UpdateProductStockAsync(
                    transactionDto.ProductId, quantity);

                if (!stockUpdated)
                {
                    await _transactionService.RevertTransactionAsync(transaction.Id);
                    return BadRequest("No se pudo actualizar el stock del producto");
                }

                return CreatedAtAction(nameof(GetTransaction), new { id = transaction.Id }, transaction);

            }
            catch (HttpRequestException ex)
            {
                await _transactionService.RevertTransactionAsync(transaction.Id);
                return StatusCode(500, "Error al comunicarse con el servicio de productos");
            }
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionDTO>> GetTransaction(Guid id)
        {
            var transaction = await _transactionService.GetTransactionByIdAsync(id);
            if (transaction == null) return NotFound();
            return Ok(transaction);
        }



    }
}