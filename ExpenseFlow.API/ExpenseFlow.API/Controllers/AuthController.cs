using ExpenseFlow.API.Auth.Dtos;
using ExpenseFlow.API.Auth.Services;
using ExpenseFlow.API.Auth.Users;
using Microsoft.AspNetCore.Mvc;

namespace ExpenseFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;

    public AuthController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        var user = InMemoryUsers.Users
            .FirstOrDefault(u =>
                u.Username == request.Username &&
                u.Password == request.Password);

        if (user is null)
            return Unauthorized(new { message = "Invalid username or password." });

        var token = _tokenService.CreateToken(user);

        return Ok(new LoginResponse
        {
            Token = token
        });
    }
}