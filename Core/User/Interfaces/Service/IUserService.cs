using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Service;
public interface IUserService
{
    Task RegisterUser(InRegisterUser input);
}