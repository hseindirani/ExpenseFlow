namespace ExpenseFlow.API.Auth.Users;

public class AppUser
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty; // for demo only
    public string Role { get; set; } = string.Empty;     // Employee / Manager
}