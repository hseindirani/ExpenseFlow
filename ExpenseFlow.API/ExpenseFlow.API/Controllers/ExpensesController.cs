using ExpenseFlow.API.Dtos;
using ExpenseFlow.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace ExpenseFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
    private static readonly List<Expense> _expenses = new();

    [HttpPost]
    public IActionResult CreateExpense([FromBody] CreateExpenseRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);
        // Server controls these fields (client cannot set them)
        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow,
            Status = "Pending"
        };

        _expenses.Add(expense);

        var response = ToResponse(expense);
        return CreatedAtAction(nameof(GetAllExpenses), new { id = expense.Id }, response);
    }

    [HttpGet]
    public IActionResult GetAllExpenses()
    {
        var responses = _expenses.Select(ToResponse).ToList();
        return Ok(responses);
    }

    private static ExpenseResponse ToResponse(Expense expense) =>
        new()
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            CreatedAt = expense.CreatedAt,
            Status = expense.Status
        };
    [HttpPut("{id:guid}/approve")]
    public IActionResult ApproveExpense(Guid id)
    {
        var expense = _expenses.FirstOrDefault(e => e.Id == id);
        if (expense is null)
            return NotFound(new { message = "Expense not found." });

        if (expense.Status != "Pending")
            return Conflict(new { message = $"Expense is already {expense.Status} and cannot be changed." });

        expense.Status = "Approved";

        return Ok(ToResponse(expense));
    }

    [HttpPut("{id:guid}/reject")]
    public IActionResult RejectExpense(Guid id)
    {
        var expense = _expenses.FirstOrDefault(e => e.Id == id);
        if (expense is null)
            return NotFound(new { message = "Expense not found." });

        if (expense.Status != "Pending")
            return Conflict(new { message = $"Expense is already {expense.Status} and cannot be changed." });

        expense.Status = "Rejected";

        return Ok(ToResponse(expense));
    }
}