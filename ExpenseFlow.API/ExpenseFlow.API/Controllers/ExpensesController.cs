using ExpenseFlow.API.Dtos;
using ExpenseFlow.API.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using ExpenseFlow.API.Data;
using Microsoft.EntityFrameworkCore;

namespace ExpenseFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ExpensesController : ControllerBase
{
   
    private readonly AppDbContext _db;

    public ExpensesController(AppDbContext db)
    {
        _db = db;
    }

    [HttpPost]
    public async Task<IActionResult> CreateExpense([FromBody] CreateExpenseRequest request)
    {
        if (!ModelState.IsValid)
            return ValidationProblem(ModelState);

        var expense = new Expense
        {
            Id = Guid.NewGuid(),
            Description = request.Description,
            Amount = request.Amount,
            CreatedAt = DateTime.UtcNow,
            Status = "Pending"
        };

        await _db.Expenses.AddAsync(expense);

        await _db.AuditLogs.AddAsync(new AuditLog
        {
            Id = Guid.NewGuid(),
            ExpenseId = expense.Id,
            Action = "Created",
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return CreatedAtAction(nameof(GetAllExpenses), new { id = expense.Id }, ToResponse(expense));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllExpenses()
    {
        var expenses = await _db.Expenses
            .OrderByDescending(e => e.CreatedAt)
            .ToListAsync();

        var responses = expenses.Select(ToResponse).ToList();
        return Ok(responses);
    }

    [HttpPut("{id:guid}/approve")]
    public async Task<IActionResult> ApproveExpense(Guid id)
    {
        var expense = await _db.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
            return NotFound(new { message = "Expense not found." });

        if (expense.Status != "Pending")
            return Conflict(new { message = $"Expense is already {expense.Status} and cannot be changed." });

        expense.Status = "Approved";

        await _db.AuditLogs.AddAsync(new AuditLog
        {
            Id = Guid.NewGuid(),
            ExpenseId = expense.Id,
            Action = "Approved",
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return Ok(ToResponse(expense));
    }

    [HttpPut("{id:guid}/reject")]
    public async Task<IActionResult> RejectExpense(Guid id)
    {
        var expense = await _db.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        if (expense is null)
            return NotFound(new { message = "Expense not found." });

        if (expense.Status != "Pending")
            return Conflict(new { message = $"Expense is already {expense.Status} and cannot be changed." });

        expense.Status = "Rejected";

        await _db.AuditLogs.AddAsync(new AuditLog
        {
            Id = Guid.NewGuid(),
            ExpenseId = expense.Id,
            Action = "Rejected",
            Timestamp = DateTime.UtcNow
        });

        await _db.SaveChangesAsync();

        return Ok(ToResponse(expense));
    }

    [HttpGet("{id:guid}/audit")]
    public async Task<IActionResult> GetAuditLogs(Guid id)
    {
        var exists = await _db.Expenses.AnyAsync(e => e.Id == id);
        if (!exists)
            return NotFound(new { message = "Expense not found." });

        var logs = await _db.AuditLogs
            .Where(l => l.ExpenseId == id)
            .OrderBy(l => l.Timestamp)
            .ToListAsync();

        return Ok(logs);
    }
    private static ExpenseResponse ToResponse(Expense expense)
    {
        return new ExpenseResponse
        {
            Id = expense.Id,
            Description = expense.Description,
            Amount = expense.Amount,
            CreatedAt = expense.CreatedAt,
            Status = expense.Status
        };
    }
}