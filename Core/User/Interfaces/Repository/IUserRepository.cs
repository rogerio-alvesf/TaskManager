using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Repository;
public interface IUserRepository
{
    Task<OutUser> AuthenticateUser(InAuthenticateUser input);
    Task<bool> UserExists(string email);
    Task RegisterUser(InRegisterUser input);
}