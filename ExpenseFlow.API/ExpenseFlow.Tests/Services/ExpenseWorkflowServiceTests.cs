using ExpenseFlow.API.Models;
using ExpenseFlow.API.Services;
using FluentAssertions;

namespace ExpenseFlow.Tests.Services;

public class ExpenseWorkflowServiceTests
{
    private readonly ExpenseWorkflowService _service = new();

    [Fact]
    public void Approve_WhenPending_SetsStatusToApproved()
    {
        var expense = new Expense { Status = "Pending" };

        _service.Approve(expense);

        expense.Status.Should().Be("Approved");
    }

    [Fact]
    public void Approve_WhenAlreadyApproved_Throws()
    {
        var expense = new Expense { Status = "Approved" };

        var act = () => _service.Approve(expense);

        act.Should().Throw<InvalidOperationException>()
            .WithMessage("Expense is already Approved*");
    }

    [Fact]
    public void Reject_WhenPending_SetsStatusToRejected()
    {
        var expense = new Expense { Status = "Pending" };

        _service.Reject(expense);

        expense.Status.Should().Be("Rejected");
    }
}