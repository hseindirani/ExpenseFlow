using ExpenseFlow.API.Auth.Users;

namespace ExpenseFlow.API.Auth.Services;

public interface ITokenService
{
    string CreateToken(AppUser user);
}