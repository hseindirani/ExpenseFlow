namespace ExpenseFlow.API.Models
{
    public class AuditLog
    {
        public Guid Id { get; set; }
        public Guid ExpenseId { get; set; }
        public string Action { get; set; } = string.Empty; // Created, Approved, Rejected
        public DateTime Timestamp { get; set; }
        public string PerformedBy { get; set; } = string.Empty;
    }
}
