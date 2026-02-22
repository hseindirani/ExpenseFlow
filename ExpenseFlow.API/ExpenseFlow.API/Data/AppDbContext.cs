using ExpenseFlow.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ExpenseFlow.API.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Expense> Expenses => Set<Expense>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
}