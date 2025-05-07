namespace TransactionService.Models
{
    public enum TransactionType
    {
        Purchase,
        Sale
    }

    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public string TransactionType { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public string Details { get; set; }
    }
}