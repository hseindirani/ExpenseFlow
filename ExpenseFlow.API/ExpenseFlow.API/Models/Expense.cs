namespace ExpenseFlow.API.Models
{
    public class Expense
    {
        
    public Guid Id { get; set; }
        public string Description { get; set; } = string.Empty;
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected
    }
}
