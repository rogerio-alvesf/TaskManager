using TaskManager.Core.Models;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Interfaces.Service;
public interface IUserService
{
    Task<OutUser> AuthenticateUser(InAuthenticateUser input);
    Task RegisterUser(InRegisterUser input);
    Task UpdateProfilePicture(string picture_user);
    Task<string> SendPasswordResetEmail();
}