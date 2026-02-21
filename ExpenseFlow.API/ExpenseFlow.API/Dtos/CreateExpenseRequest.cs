using System.ComponentModel.DataAnnotations;

namespace ExpenseFlow.API.Dtos;

public class CreateExpenseRequest
{
    [Required]
    [MinLength(3)]
    public string Description { get; set; } = string.Empty;

    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }
}