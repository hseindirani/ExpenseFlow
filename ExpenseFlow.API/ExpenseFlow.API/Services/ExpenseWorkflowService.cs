using ExpenseFlow.API.Models;

namespace ExpenseFlow.API.Services;

public class ExpenseWorkflowService
{
    public void Approve(Expense expense)
    {
        EnsurePending(expense);
        expense.Status = "Approved";
    }

    public void Reject(Expense expense)
    {
        EnsurePending(expense);
        expense.Status = "Rejected";
    }

    private static void EnsurePending(Expense expense)
    {
        if (expense.Status != "Pending")
            throw new InvalidOperationException($"Expense is already {expense.Status} and cannot be changed.");
    }
}