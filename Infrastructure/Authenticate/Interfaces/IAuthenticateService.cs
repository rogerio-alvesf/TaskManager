using TaskManager.Core.Models;

namespace TaskManager.Infrastructure.Authenticate;

public interface IAuthenticateService 
{
    Task<bool> AuthenticateAsync(InAuthenticateUser input);
    Task<bool> UserExists(string email);
    string GenerateToken(int id, string email);
}