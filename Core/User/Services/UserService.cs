using TaskManager.Core.Interfaces.Repository;
using TaskManager.Core.Interfaces.Service;
using TaskManager.Core.Models;
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

    public async Task<OutUser> AuthenticateUser(InAuthenticateUser input)
    {
        if (!await _userRepository.UserExists(input.Email))
            throw new UnauthorizedException("Authentication error");

        var user = await _userRepository.AuthenticateUser(input);

        if (user == null)
            throw new NotFoundException("Authentication error");

        return user;
    }

    public async Task RegisterUser(InRegisterUser input)
    {
        if (await _userRepository.UserExists(input.Email_User))
            throw new ConflictException("sorry, something went wrong");

        await _userRepository.RegisterUser(input);
    }
}