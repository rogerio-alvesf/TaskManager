using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Repository;
public interface IUserRepository
{
    Task RegisterUser(InRegisterUser input);
}