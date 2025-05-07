namespace TransactionService.DTOs
{
    public class TransactionDTO
    {
        public Guid Id { get; set; }
        public DateTime TransactionDate { get; set; }
        public string TransactionType { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Details { get; set; }
    }

    public class CreateTransactionDTO
    {
        public string TransactionType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public string Details { get; set; }
    }
}