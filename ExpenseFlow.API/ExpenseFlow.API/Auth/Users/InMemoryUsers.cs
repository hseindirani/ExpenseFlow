namespace ExpenseFlow.API.Auth.Users;

public static class InMemoryUsers
{
    public static readonly List<AppUser> Users =
    [
        new AppUser { Username = "employee", Password = "123", Role = "Employee" },
        new AppUser { Username = "manager",  Password = "123", Role = "Manager" },
        new AppUser {Username = "blomst", Password = "123", Role = "Employee"},
        new AppUser {Username = "natascha", Password = "123", Role = "Manager"},
    ];
}