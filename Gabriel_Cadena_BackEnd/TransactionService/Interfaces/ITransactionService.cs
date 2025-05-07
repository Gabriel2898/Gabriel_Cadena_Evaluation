using ProductService.Helpers;
using TransactionService.DTOs;

namespace TransactionService.Interfaces
{
    public interface ITransactionService
    {
        Task<PagedResponse<TransactionDTO>> GetTransactionsAsync(
            PaginationFilter paginationFilter,
            DateTime? startDate,
            DateTime? endDate);

        Task<TransactionDTO> CreateTransactionAsync(CreateTransactionDTO transactionDto);

        Task<bool> RevertTransactionAsync(Guid transactionId);
        Task<TransactionDTO> GetTransactionByIdAsync(Guid id);
    }
}