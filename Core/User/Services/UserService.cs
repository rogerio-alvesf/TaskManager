using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models.Input;
using TaskManager.Core.Models.Output;

namespace TaskManager.Core.Services;
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task RegisterUser(InRegisterUser input)
    {
        if (await _userRepository.CheckUserExists(input.Email_User, input.Password_User))
            throw new ConflictException("sorry, something went wrong");

        await _userRepository.RegisterUser(input);
    }
}