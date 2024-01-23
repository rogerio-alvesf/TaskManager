using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Repository;
public interface IUserRepository
{
    Task<bool> CheckUserExists(string email, string password);
    Task RegisterUser(InRegisterUser input);
}