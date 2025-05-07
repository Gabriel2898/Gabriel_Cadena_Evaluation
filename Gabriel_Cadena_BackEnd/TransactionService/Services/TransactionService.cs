using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ProductService.Helpers;
using TransactionService.Data;
using TransactionService.DTOs;
using TransactionService.Interfaces;
using TransactionService.Models;

namespace TransactionService.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly TransactionContext _context;
        private readonly IMapper _mapper;
        private readonly IProductServiceClient _productServiceClient;

        public TransactionService(
            TransactionContext context,
            IMapper mapper,
            IProductServiceClient productServiceClient)
        {
            _context = context;
            _mapper = mapper;
            _productServiceClient = productServiceClient;
        }


public async Task<PagedResponse<TransactionDTO>> GetTransactionsAsync(
    PaginationFilter paginationFilter,
    DateTime? startDate,
    DateTime? endDate)
        {
            try
            {
                var query = _context.Transactions.AsQueryable();

                if (startDate.HasValue)
                    query = query.Where(t => t.TransactionDate >= startDate.Value.Date);

                if (endDate.HasValue)
                    query = query.Where(t => t.TransactionDate <= endDate.Value.Date.AddDays(1).AddTicks(-1));

                var totalRecords = await query.CountAsync();

                var transactions = await query
                    .OrderByDescending(t => t.TransactionDate)
                    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
                    .Take(paginationFilter.PageSize)
                    .Select(t => new TransactionDTO
                    {
                        Id = t.Id,
                        TransactionDate = t.TransactionDate,
                        TransactionType = t.TransactionType.ToString(),
                        ProductId = t.ProductId,
                        Quantity = t.Quantity,
                        UnitPrice = t.UnitPrice,
                        TotalPrice = t.TotalPrice,
                        Details = t.Details
                    })
                    .ToListAsync();

               
                var semaphore = new SemaphoreSlim(5); 

                var productTasks = transactions.Select(async t =>
                {
                    await semaphore.WaitAsync();
                    try
                    {
                        return await _productServiceClient.GetProductAsync(t.ProductId);
                    }
                    catch (Exception ex)
                    {
                        
                        return null;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }).ToList();

                var products = await Task.WhenAll(productTasks);

                for (int i = 0; i < transactions.Count; i++)
                {
                    transactions[i].ProductName = products[i]?.Name ?? "Producto no disponible";
                }

                return new PagedResponse<TransactionDTO>
                {
                    Data = transactions,
                    PageNumber = paginationFilter.PageNumber,
                    PageSize = paginationFilter.PageSize,
                    TotalRecords = totalRecords
                };
            }
            catch 
            {
                throw;
            }
        }
        public async Task<TransactionDTO> CreateTransactionAsync(CreateTransactionDTO transactionDto)
        {
            if (!Enum.TryParse<TransactionType>(transactionDto.TransactionType, out var transactionType))
                throw new ArgumentException("Tipo de transacción no válido");

            var transaction = new Transaction
            {
                TransactionType = transactionType.ToString(),
                ProductId = transactionDto.ProductId,
                Quantity = transactionDto.Quantity,
                UnitPrice = transactionDto.UnitPrice,
                TotalPrice = transactionDto.Quantity * transactionDto.UnitPrice,
                Details = transactionDto.Details
            };

            _context.Transactions.Add(transaction);
            await _context.SaveChangesAsync();

            return _mapper.Map<TransactionDTO>(transaction);
        }
        public async Task<TransactionDTO> GetTransactionByIdAsync(Guid id)
        {
            var transaction = await _context.Transactions
                .FirstOrDefaultAsync(t => t.Id == id);

            if (transaction == null)
                return null;

            return _mapper.Map<TransactionDTO>(transaction);
        }
        public async Task<bool> RevertTransactionAsync(Guid transactionId)
        {
            var transaction = await _context.Transactions.FindAsync(transactionId);
            if (transaction == null) return false;

            _context.Transactions.Remove(transaction);
            return await _context.SaveChangesAsync() > 0;
        }
    }
}